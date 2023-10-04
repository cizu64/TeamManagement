using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using System.Web;
using TaskManagement.Web.Extensions;

namespace TaskManagement.Web.Filters
{
    public record TokenValidationResult
    {
        public string role { get; set; }
        public bool isvalid { get; set; }
        public int usid { get; set; }
    }
    public class TokenAuth : IAsyncAuthorizationFilter
    {
        private readonly IHttpClientFactory _client;
        public static string Role;
        public TokenAuth(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Cookies["token"];
            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ValidateToken?token={token}", "TokenValidation", token, null);
            if (request.IsSuccessStatusCode)
            {
                var result = await request.Content.ReadFromJsonAsync<TokenValidationResult>();
                if (result.isvalid && result.role.Equals(Role, StringComparison.OrdinalIgnoreCase))
                {
                    context.HttpContext.Response.Cookies.Append("usid", result.usid.ToString());
                    return;
                }
                else
                {
                    //context.Result = new UnauthorizedResult();
                    context.HttpContext.Response.Redirect("/login"); //redirect to login page
                    return;
                }
            }
            else
            {
                context.HttpContext.Response.Redirect("/login"); //redirect to login page
                return;
            }
        }
    }
}
