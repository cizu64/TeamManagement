using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.Infrastructure;
using TaskManagement.Infrastructure.Repository;

namespace TaskManagement.WebAPI.BackgroundServices
{
    public class CustomLogger : BackgroundService
    {
        private readonly PeriodicTimer timer;
        private readonly TaskManagementContext context;
        private readonly IConfiguration _configuration;
        public readonly ILogger<CustomLogger> _logger;
        public CustomLogger(IConfiguration configuration, ILogger<CustomLogger> logger)
        {
            _logger = logger;
            timer = new PeriodicTimer(TimeSpan.FromSeconds(20));
            _configuration = configuration;
            var optionsBuilder = new DbContextOptionsBuilder<TaskManagementContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionString"]);
            context = new TaskManagementContext(optionsBuilder.Options);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(await timer.WaitForNextTickAsync(stoppingToken))
            {
                foreach (var log in Logger.Logs)
                {
                    await context.Log.AddAsync(log);
                    
                }
                await context.SaveChangesAsync();

                _logger.LogInformation("the log queue is being persisited");
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
