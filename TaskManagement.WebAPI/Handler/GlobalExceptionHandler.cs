using Microsoft.AspNetCore.Diagnostics;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.WebAPI.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Logger.LogIt(new Domain.Entities.Log(exception.StackTrace, exception.Message));
            return ValueTask.FromResult(false); //return false so in the development environment, the developer exception page is shown.
        }
    }
}
