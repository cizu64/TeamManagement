using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Attributes;

namespace TaskManagement.Web.Pages
{
    [TokenAuthorize(Role:"TeamLead")]
    public class TeamMember : PageModel
    {
        private readonly ILogger<TeamMember> _logger;

        public TeamMember(ILogger<TeamMember> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            //check role
            var role = Request.Cookies.ContainsKey("role") ? Request.Cookies["role"].ToString() : "";
            if (string.IsNullOrEmpty(role))
            {
                return RedirectToPage("/login");
            }
            if (role.Equals("TeamLead"))
            {
                return RedirectToPage("/TeamLead");
            }
            return Page();
        }
    }
}