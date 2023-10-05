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
    public class CreateProjectTaskModel : PageModel
    {
        private readonly TeamLead teamLead;

        public CreateProjectTaskModel(TeamLead teamLead)
        {
            this.teamLead = teamLead;
        }

        public async Task<IActionResult> OnGet()
        {
            string token = Request.Cookies["token"].ToString();
            APIResult projects = await teamLead.ViewProjects(token);
            ViewData["projects"] = projects.detail as IList<Projects>;
            return Page();
        }

        [BindProperty]
        public ProjectTaskDTO ProjectTaskDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string token = Request.Cookies["token"].ToString();
            APIResult project = await teamLead.AddTask(token, ProjectTaskDTO.ProjectId, ProjectTaskDTO.assignedTeamMemberIds, ProjectTaskDTO.Title, ProjectTaskDTO.TaskDescription, ProjectTaskDTO.Priority, ProjectTaskDTO.FromDate, ProjectTaskDTO.ToDate);
            if (project.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = project.detail; //display the detail of the error
                return Page();
            }
            ViewData["suc"] = project.detail;
            return Page();
        }
    }
    public enum Priority
    {
        HIGH,
        MEDIUM,
        LOW
    }
    public class ProjectTaskDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string[] assignedTeamMemberIds { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public Priority Priority { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string TaskDescription { get; set; }
    }
}