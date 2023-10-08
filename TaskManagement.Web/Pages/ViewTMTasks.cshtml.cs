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
    public class ViewTMTasksModel : PageModel
    {
        private readonly Services.TeamMember teamMember;

        public ViewTMTasksModel(Services.TeamMember teamMember)
        {
            this.teamMember = teamMember;
        }

        public async Task<IActionResult> OnGet()
        {  
            string token = Request.Cookies["token"].ToString();
            APIResult tasks = await teamMember.ViewTasks(token);
            if (tasks.statusCode != (int)HttpStatusCode.OK)
            {
                if (tasks.statusCode == (int)HttpStatusCode.Unauthorized)
                {
                    return RedirectToPage("/login");
                }
                ViewData["err"] = tasks.detail; //display the detail of the error
                return Page();
            }
            Tasks = (ProjectTask[])tasks.detail;
            return Page();
        }

        [BindProperty]
        public ProjectTask[] Tasks{ get; set; }

    }

  
}