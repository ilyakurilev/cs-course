using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Reminder.WebApi
{
    using Microsoft.AspNetCore.Http;
    using Reminder.Storage;
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.Memory;
    using System;
    using System.Threading.Tasks;

    public class Startup
    { 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IAsyncReminderStorage, AsyncReminderStorage>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(ReminderExceptionHandling);
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }

        private static async Task ReminderExceptionHandling(HttpContext context, Func<Task> next)
        {
            try
            {
                await next();
            }
            catch (ReminderItemNotFoundException)
            {
                context.Response.StatusCode = 404;
            }
            catch (ReminderItemAlreadyExistsException)
            {
                context.Response.StatusCode = 409;
            }
        }

    }
}
