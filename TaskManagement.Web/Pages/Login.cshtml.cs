using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Encodings.Web;
using System.Web;
using TaskManagement.Web.Common;
using TaskManagement.Web.Extensions;
using TaskManagement.Web.Filters;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TeamLead teamLead;
        IHttpClientFactory _client;
        public LoginModel(TeamLead teamLead, IHttpClientFactory client)
        {
            this.teamLead = teamLead;
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {

            return Page();
        }
        [BindProperty]
        public LoginDTO LoginDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            APIResult token = await teamLead.Login(LoginDTO.Email, LoginDTO.Password);
            if (token.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = token.detail; //display the detail of the error
                return Page();
            }
            Response.Cookies.Append("token", (string)token.detail,new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure=true
            });

            //validate token and get the role (can be return form the backend. Just for Demo purposes)
            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ValidateToken?token={(string)token.detail}", "TokenValidation", token, null);
            if (request.IsSuccessStatusCode)
            {
                var result = await request.Content.ReadFromJsonAsync<TokenValidationResult>();
                if (result!=null && result.isvalid)
                {
                    Response.Cookies.Append("role", result.role);
                }
            }
            return RedirectToPage("/index");
        }
    }
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
