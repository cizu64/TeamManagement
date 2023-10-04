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
    public class ViewProjectModel : PageModel
    {
        private readonly TeamLead teamLead;

        public ViewProjectModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }

        public int ProjectId { get; set; }
        public async Task<IActionResult> OnGet(int projectId)
        {  
            string token = Request.Cookies["token"].ToString();
            APIResult project = await teamLead.ViewProject(token, projectId);
            if (project.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = project.detail; //display the detail of the error
                return Page();
            }
            ViewProject = (ViewProject) project.detail;
            return Page();
        }

        [BindProperty]
        public ViewProject ViewProject { get; set; }

    }

}