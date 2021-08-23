using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reminder.Domain;
using Reminder.Receiver;
using Reminder.Receiver.Telegram;
using Reminder.Sender;
using Reminder.Sender.Telegram;
using Reminder.WebApi.Services;


namespace Reminder.WebApi
{
    using Storage;
    using Storage.SqlServer.Ef;
    using Storage.Exceptions;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ReminderStorageContext>(_ =>
                _.UseSqlServer(_configuration.GetConnectionString("Database")));
            services.AddScoped<IReminderStorage, ReminderStorage>();

            services.AddSingleton(_ =>
                _configuration.GetSection("Scheduler").Get<ReminderSchedulerSettings>()
            );
            services.AddSingleton<IReminderReceiver, ReminderReceiver>(_ =>
                new ReminderReceiver(_configuration.GetConnectionString("Telegram"))
            );
            services.AddSingleton<IReminderSender, ReminderSender>(_ =>
                new ReminderSender(_configuration.GetConnectionString("Telegram"))
            );
            
            services.AddScoped<ReminderScheduler>();
            services.AddHostedService<SchedulerService>();
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
