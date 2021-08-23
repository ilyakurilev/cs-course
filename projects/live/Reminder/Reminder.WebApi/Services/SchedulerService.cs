using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reminder.Domain;

namespace Reminder.WebApi.Services
{
    public class SchedulerService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        
        public SchedulerService(IServiceProvider provider)
        {
            _provider = provider;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _provider.CreateScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<ReminderScheduler>();
            var settings = scope.ServiceProvider.GetRequiredService<ReminderSchedulerSettings>();
            await scheduler.StartAsync(settings, stoppingToken);
        }
    }
}