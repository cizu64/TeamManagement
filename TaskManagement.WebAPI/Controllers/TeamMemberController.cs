using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.WebAPI.DTO;
using TaskManagement.WebAPI.Security;

namespace TaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamMemberController : ControllerBase
    {
        IGenericRepository<TeamLead> _teamLeadRepo;
        private readonly IGenericRepository<Project> _projectRepo;
        private readonly IGenericRepository<ProjectTask> _projectTaskRepo;
        private readonly IGenericRepository<TeamMember> _teamMemberRepo;
        private readonly IGenericRepository<Todo> _todoRepo;
        private readonly JwtAuth auth;

        public TeamMemberController(IGenericRepository<TeamLead> teamLeadRepo, IGenericRepository<Project> projectRepo, IGenericRepository<ProjectTask> projectTaskRepo, IGenericRepository<TeamMember> teamMemberRepo, IGenericRepository<Todo> todoRepo, JwtAuth auth)
        {
            _teamLeadRepo = teamLeadRepo;
            _projectRepo = projectRepo;
            _projectTaskRepo = projectTaskRepo;
            _teamMemberRepo = teamMemberRepo;
            _todoRepo = todoRepo;
            this.auth = auth;
        }
        [HttpGet, Authorize(Policy = "TeamMemberOnly")]
        public async Task<IActionResult> ViewProjects()
        {
            int teamMemberId; //current loggedin team member id
            int.TryParse(User.Identity.Name, out teamMemberId);
            var tm = await _teamMemberRepo.GetByIdAsync(teamMemberId, "TeamLead", "TeamLead.Projects");
            var projects = tm.TeamLead.Projects;
            if (projects == null) return Problem(detail: "No projects", statusCode: (int)HttpStatusCode.InternalServerError);
            List<object> lstProjects = new();
            foreach (var p in projects)
            {
                string newAssignedTMIds = p.AssignedTeamMemberIds;
                if (!p.AssignedTeamMemberIds.StartsWith("_"))
                {
                    newAssignedTMIds = $"_{p.AssignedTeamMemberIds}";
                }
                if (newAssignedTMIds.Contains($"_{teamMemberId}_"))
                {
                    lstProjects.Add(new
                    {
                        p.Id,
                        p.Name,
                        p.Description,
                        p.DateCreated,
                        p.IsActive
                    });
                }
            }

            return Ok(lstProjects);
        }
        [HttpGet("{projectId:int}"), Authorize(Policy = "TeamMemberOnly")]
        public async Task<IActionResult> ViewProjectTask(int projectId)
        {
            int teamMemberId; //current loggedin team member id
            int.TryParse(User.Identity.Name, out teamMemberId);
            var tm = await _teamMemberRepo.GetByIdAsync(teamMemberId, "TeamLead", "TeamLead.Projects", "TeamLead.Projects.ProjectTasks");
            var tasks = tm.TeamLead.Projects.Select(t => t.ProjectTasks).FirstOrDefault();
            List<object> lstTasks = new();
            foreach (var t in tasks)
            {
                string newAssignedTMIds = t.AssignedTo;
                if (!t.AssignedTo.StartsWith("_"))
                {
                    newAssignedTMIds = $"_{t.AssignedTo}";
                }
                if (newAssignedTMIds.Contains($"_{teamMemberId}_"))
                {
                    lstTasks.Add(new
                    {
                        t.Id,
                        t.Project.Name,
                        t.Title,
                        t.TaskDescription,
                        t.Priority,
                        Status = t.IsActive,
                        StartDate = t.FromDate,
                        EndDate = t.ToDate,
                        t.IsCompleted,
                        t.DateCreated
                    });
                }
            }
            return Ok(lstTasks);
        }

        //team member sign in
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO dto)
        {
            string token = await auth.AuthenticateTeamMember(dto.Email, dto.Password);
            return Ok(token);
        }
    }
}
