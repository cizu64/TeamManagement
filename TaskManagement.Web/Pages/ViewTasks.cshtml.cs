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
    public class ViewTasksModel : PageModel
    {
        private readonly TeamLead teamLead;

        public ViewTasksModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }

        public async Task<IActionResult> OnGet()
        {  
            string token = Request.Cookies["token"].ToString();
            APIResult project = await teamLead.ViewTasks(token);
            if (project.statusCode != (int)HttpStatusCode.OK)
            {
                if (project.statusCode == (int)HttpStatusCode.Unauthorized)
                {
                    return RedirectToPage("/login");
                }
                ViewData["err"] = project.detail; //display the detail of the error
                return Page();
            }
            Tasks = (IReadOnlyList<ProjectTask>)project.detail;
            return Page();
        }

        [BindProperty]
        public IReadOnlyList<ProjectTask> Tasks{ get; set; }

    }

  
}