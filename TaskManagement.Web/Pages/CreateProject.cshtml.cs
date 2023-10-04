using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagement.Web.Attributes;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;

namespace TaskManagement.Web.Pages
{
    [TokenAuthorize(Role: "TeamLead")]
    public class CreateProjectModel : PageModel
    {
        private readonly TeamLead teamLead;

        public CreateProjectModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }


        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProjectDTO ProjectDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string token = Request.Cookies["token"].ToString();
            int usid = int.Parse(Request.Cookies["usid"]);
            APIResult project = await teamLead.AddProject(token, ProjectDTO.Name, ProjectDTO.Description, usid, ProjectDTO.assignedTeamMemberIds);
            if (project.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = project.detail; //display the detail of the error
                return Page();
            }
            ViewData["suc"] = project.detail;
            return Page();
        }
    }

    public class ProjectDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string[] assignedTeamMemberIds { get; set; }
    }
}