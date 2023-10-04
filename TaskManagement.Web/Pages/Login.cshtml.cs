using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Encodings.Web;
using System.Web;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TeamLead teamLead;

        public LoginModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
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
            Response.Cookies.Append("token", token.detail,new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure=true
            });
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
