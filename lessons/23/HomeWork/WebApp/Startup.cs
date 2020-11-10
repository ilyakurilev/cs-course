using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using WebApp.CityStorage;
using WebApp.CityStorage.Memory;

namespace WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICityStorage>(_ => new CitiesStorage(new[]
                {
                    new City(Guid.NewGuid(), "Moscow", "The capital of Russia", 16_000_000),
                    new City(Guid.NewGuid(), "Tokio", "The capital of Japan", 15_000_000)
                })
            );

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
