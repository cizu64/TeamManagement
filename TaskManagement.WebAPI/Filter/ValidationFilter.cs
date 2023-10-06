using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagement.WebAPI.Filter
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
