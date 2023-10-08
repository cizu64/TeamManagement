using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly TeamLead teamLead;
        private readonly Country country;
        public RegisterModel(TeamLead teamLead, Country country)
        {
            this.teamLead = teamLead;
            this.country = country;
        }
        public async Task<IActionResult> OnGet()
        {
            var countries = await country.Countries();
            ViewData["countries"] = countries.detail as VM.Countries[];
            return Page();
        }
        [BindProperty]
        public RegisterDTO RegisterDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var countries = await country.Countries();
            ViewData["countries"] = countries.detail as VM.Countries[];
            if (!ModelState.IsValid)
            {
                return Page();
            }
            APIResult register = await teamLead.Register(RegisterDTO.CountryId,RegisterDTO.Email,RegisterDTO.Firstname,RegisterDTO.Lastname, RegisterDTO.Password);
            if (register.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = register.detail; //display the detail of the error
                return Page();
            }
            //login
            APIResult token = await teamLead.Login(RegisterDTO.Email, RegisterDTO.Password);
            Response.Cookies.Append("token", (string)token.detail, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            });
            return RedirectToPage("/Index");
        }
    }
    public class RegisterDTO
    {
        [Required]
        public int CountryId { get; set; }
        [Required]
        [EmailAddress]
        public  string Email { get; set; }
        [Required]

        public  string Firstname { get; set; }
        [Required]

        public  string Lastname { get; set; }
        [Required]
        [MaxLength(8)]
        public  string Password { get; set; }
    }
}
