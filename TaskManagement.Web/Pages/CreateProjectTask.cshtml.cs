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
            ViewData["projects"] = projects.detail as IReadOnlyList<AllProject>;

            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as TeamMembers[];
            return Page();
        }

        [BindProperty]
        public ProjectTaskDTO ProjectTaskDTO { get; set; }
        public async Task<IActionResult> OnPost()
        {
            
            string token = Request.Cookies["token"].ToString();
            APIResult projects = await teamLead.ViewProjects(token);
            ViewData["projects"] = projects.detail as IReadOnlyList<AllProject>;

            var teamMembers = await teamLead.ViewTeamMembers(token);
            ViewData["tm"] = teamMembers.detail as TeamMembers[];

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var fromDate = ProjectTaskDTO.FromDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var toDate = ProjectTaskDTO.ToDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            APIResult addTask = await teamLead.AddTask(token, ProjectTaskDTO.ProjectId, ProjectTaskDTO.AssignedTo, ProjectTaskDTO.Title, ProjectTaskDTO.TaskDescription, ProjectTaskDTO.Priority, fromDate, toDate);

            if (addTask.statusCode != (int)HttpStatusCode.OK)
            {
                ViewData["err"] = addTask.detail; //display the detail of the error
                return Page();
            }
       
            ViewData["suc"] = addTask.detail;
            return Page();
        }
    }
  
    public class ProjectTaskDTO
    {
        public string Title { get; set; }
        public string[]? AssignedTo { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string TaskDescription { get; set; }

        
    }
}