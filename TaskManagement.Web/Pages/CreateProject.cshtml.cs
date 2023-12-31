using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagement.Web.Attributes;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;
using static TaskManagement.Web.VM.TeamMemberVM;

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
            string token = Request.Cookies["token"].ToString();
            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as TeamMembers[];
            return Page();
        }

        [BindProperty]
        public ProjectDTO ProjectDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            string token = Request.Cookies["token"].ToString();
            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as TeamMembers[];
            if (!ModelState.IsValid)
            {
                return Page();
            }
            APIResult project = await teamLead.AddProject(token, ProjectDTO.Name, ProjectDTO.Description, ProjectDTO.assignedTeamMemberIds);
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
        [Required(ErrorMessage = "The Project Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="The Project Description is Required")]
        public string Description { get; set; }
        
        public string[]? assignedTeamMemberIds { get; set; }
    }
}