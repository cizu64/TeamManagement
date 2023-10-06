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
    public class AssignProjectModel : PageModel
    {
        private readonly TeamLead teamLead;

        public AssignProjectModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }

        public async Task<IActionResult> OnGet()
        {
            string token = Request.Cookies["token"].ToString();
            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as VM.TeamMembers[];

            APIResult projects = await teamLead.ViewProjects(token);
            ViewData["projects"] = projects.detail as IReadOnlyList<AllProject>;
            return Page();
        }

        [BindProperty]
        public AssignProjectDTO AssignProjectDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            string token = Request.Cookies["token"].ToString();
            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as VM.TeamMembers[];

            APIResult projects = await teamLead.ViewProjects(token);
            ViewData["projects"] = projects.detail as IReadOnlyList<AllProject>;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            APIResult teamMember = await teamLead.AssignProject(token, AssignProjectDTO.projectId, AssignProjectDTO.teamMemberId);
            if (teamMember.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = teamMember.detail; //display the detail of the error
                return Page();
            }
            ViewData["suc"] = teamMember.detail;
            return Page();
        }
    }

    public record AssignProjectDTO
    {
        [Required]
        public required int teamMemberId { get; set; }
        [Required]
        public required int projectId { get; set; }
       
    }
}