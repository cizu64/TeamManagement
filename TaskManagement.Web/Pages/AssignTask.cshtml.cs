using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using TaskManagement.Web.Attributes;
using TaskManagement.Web.Common;
using TaskManagement.Web.Services;
using TaskManagement.Web.VM;
using static TaskManagement.Web.VM.TeamMemberVM;

namespace TaskManagement.Web.Pages
{
    [TokenAuthorize(Role: "TeamLead")]
    public class AssignTaskModel : PageModel
    {
        private readonly TeamLead teamLead;

        public AssignTaskModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }

        public async Task<IActionResult> OnGet()
        {
            string token = Request.Cookies["token"].ToString();
            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as TeamMembers[];

            APIResult projectTasks = await teamLead.ViewProjectTasks(token);
            ViewData["ptask"] = projectTasks.detail as IReadOnlyList<AllProject>;
            return Page();
        }

        [BindProperty]
        public AssignProjectTaskDTO AssignProjectTaskDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            string token = Request.Cookies["token"].ToString();
            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as TeamMembers[];

            APIResult projectTasks = await teamLead.ViewProjectTasks(token);
            ViewData["ptask"] = projectTasks.detail as IReadOnlyList<AllProject>;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            APIResult teamMember = await teamLead.AssignProjectTask(token, AssignProjectTaskDTO.projectTaskId, AssignProjectTaskDTO.teamMemberId);
            if (teamMember.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = teamMember.detail; //display the detail of the error
                return Page();
            }
            ViewData["suc"] = teamMember.detail;
            return Page();
        }
    }

    public record AssignProjectTaskDTO
    {
        [Required]
        public required int teamMemberId { get; set; }
        [Required]
        public required int projectTaskId { get; set; }
       
    }
}