using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Web.Attributes;

namespace TaskManagement.Web.Pages
{
    [TokenAuthorize(Role:"TeamLead")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
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
            if (role.Equals("TeamMember"))
            {
                return RedirectToPage("/TeamMember");
            }
            return Page();
        }
    }
}