using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using WebApp.Models;
using WebApp.Storage;

namespace WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStorage<City>>(provider => 
            {
                var cities = new Storage.Memory.Storage<City>();
                cities.Add(new City(Guid.NewGuid(), "Moscow", "The capital of Russia", 16_000_000));
                cities.Add(new City(Guid.NewGuid(), "Tokio", "The capital of Japan", 15_000_000));
                return cities;
            });

            services
                .AddControllers()
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("cities",
                    new OpenApiInfo
                    {
                        Version = "v2",
                        Title = "Cities API",
                    }
                )
            );
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("cities/swagger.json", "Cities API"));
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
