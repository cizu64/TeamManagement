using Microsoft.Extensions.Logging;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Infrastructure
{
    public class TaskManagementContextSeed
    {
        public static async Task SendAsync(TaskManagementContext context)
        {
            try
            {
                if (!context.Country.Any())
                {
                    context.Country.Add(new Domain.Entities.Country("USA"));
                }
                await context.SaveChangesAsync();
                //Logger.LogIt(new Domain.Entities.Log("Database seed was successful"));
            }
            catch (Exception ex)
            {
                //var log = loggerFactory.CreateLogger<TaskManagementContextSeed>();
                //using the custom defined log              
                Logger.LogIt(new Domain.Entities.Log(ex.StackTrace, ex.Message));
            }
        }
    }
}
