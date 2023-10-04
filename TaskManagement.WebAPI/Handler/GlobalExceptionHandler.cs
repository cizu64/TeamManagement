using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.WebAPI.Filters
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Logger.LogIt(new Domain.Entities.Log(exception.StackTrace, exception.Message));
            await httpContext.Response.WriteAsJsonAsync(new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Detail = "An unexpected error occurred",
            });
            return await ValueTask.FromResult(false); //return false so in the development environment, the developer exception page is shown.
        }
    }
}
