using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagement.Web.Attributes;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;
using TaskManagement.Web.VM;

namespace TaskManagement.Web.Pages
{
    [TokenAuthorize(Role: "TeamLead")]
    public class ViewProjectsModel : PageModel
    {
        private readonly TeamLead teamLead;

        public ViewProjectsModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }

        public async Task<IActionResult> OnGet()
        {  
            string token = Request.Cookies["token"].ToString();
            APIResult project = await teamLead.ViewProjects(token);
            if (project.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = project.detail; //display the detail of the error
                return Page();
            }
            Projects = (IList<Projects>)project.detail;
            return Page();
        }

        [BindProperty]
        public IList<Projects> Projects{ get; set; }

    }

  
}