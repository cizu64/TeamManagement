using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.WebAPI.DTO;
using TaskManagement.WebAPI.Filter;
using TaskManagement.WebAPI.Security;

namespace TaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamLeadController : ControllerBase
    {
        IGenericRepository<TeamLead> _teamLeadRepo;
        private readonly IGenericRepository<Project> _projectRepo;
        private readonly IGenericRepository<ProjectTask> _projectTaskRepo;
        private readonly IGenericRepository<TeamMember> _teamMemberRepo;
        private readonly JwtAuth auth;

        public TeamLeadController(IGenericRepository<TeamLead> teamLeadRepo, IGenericRepository<Project> projectRepo, IGenericRepository<ProjectTask> projectTaskRepo, IGenericRepository<TeamMember> teamMemberRepo, JwtAuth auth)
        {
            _teamLeadRepo = teamLeadRepo;
            _projectRepo = projectRepo;
            _projectTaskRepo = projectTaskRepo;
            _teamMemberRepo = teamMemberRepo;
            this.auth = auth;
        }
        //create account
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO dto)
        {
            await _teamLeadRepo.AddAsync(new TeamLead(dto.CountryId,dto.Email, dto.Firstname, dto.Lastname, dto.Password));
            var checkEmailExists = await _teamLeadRepo.AnyAsync(e => e.Email == dto.Email);
            if(checkEmailExists) return Problem(detail: "Email already exists", statusCode: (int)HttpStatusCode.InternalServerError);
            await _teamLeadRepo.UnitOfWork.SaveAsync();
            //return created object 
            return Ok("Account created");
        }
        //sign in
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO dto)
        {
            string token = await auth.AuthenticateTeamLead(dto.Email, dto.Password);
            if (token == string.Empty) return Problem(detail: "Invalid email or password", statusCode: (int)HttpStatusCode.InternalServerError);
            //return jwt token  
            return Ok(token);
        }

        //create project
        [HttpPost, Authorize(Policy = "TeamLeadOnly"), ValidationFilter]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO dto)
        {
            int teamLeadId;
            int.TryParse(User.Identity.Name, out teamLeadId);
            string Ids = "";
            if (dto.AssignedTeamMemberIds!=null && dto.AssignedTeamMemberIds.Length != 0)
            {
                foreach (var ids in dto.AssignedTeamMemberIds)
                {
                    Ids += $"{ids}_";
                }
            }
            await _projectRepo.AddAsync(new Project(dto.Name, teamLeadId, dto.Description, Ids));
            await _projectRepo.UnitOfWork.SaveAsync();
            return Ok($"""Project({dto.Name}) created successfully"""); //using the new c# raw string literal 
        }

        //view project
        [HttpGet("{Id:int}"), Authorize(Policy= "TeamLeadOnly")]
        public async Task<IActionResult> ViewProject(int Id)
        {
            int teamLeadId = 0;
            if(!int.TryParse(User.Identity.Name,out teamLeadId)) return Problem(detail: "Anthorization required", statusCode: (int)HttpStatusCode.InternalServerError);
            var project = await _projectRepo.Get(p => p.Id == Id && p.TeamLeadId == teamLeadId);
            var teamMembers = project.AssignedTeamMemberIds.Split("_");
            string Members = "";
            foreach (var id in teamMembers)
            {
                var tm = await _teamMemberRepo.Get(t => t.Id == id);
                Members += $"{tm.Firstname} {tm.Lastname} -> ";

            }
            return Ok(new
            {
                project.Name,
                project.Description,
                project.DateCreated,
                AssignedTeamMemberIds = Members
            });
        }

        //view projects
        [HttpGet, Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> ViewProjects()
        {
            int teamLeadId = 0;
            int.TryParse(User.Identity.Name, out teamLeadId);

            var projects = await _projectRepo.GetAll(p => p.TeamLeadId == teamLeadId);

            var teamMembers = projects.Select(t => t.AssignedTeamMemberIds).Split("_");
            string Members = "";
            foreach (var id in teamMembers)
            {
                var tm = await _teamMemberRepo.Get(t => t.Id == id);
                Members += $"{tm.Firstname} {tm.Lastname} -> ";

            }
            return Ok(projects.Select(p => new
            {
                p.Name,
                p.Description,
                p.DateCreated,
                AssignedTeamMemberIds = Members
            }));
        }

        //create project task
        [HttpPost, Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> CreateProjectTask([FromBody] CreateProjectTaskDTO dto)
        {
            int teamLeadId;
            int.TryParse(User.Identity.Name, out teamLeadId);
            string Ids = "";
            if (dto.AssignedTo != null && dto.AssignedTo.Length != 0)
            {
                foreach (var ids in dto.AssignedTo)
                {
                    Ids += $"{ids}_";
                }
            }
            await _projectTaskRepo.AddAsync(new ProjectTask(teamLeadId,dto.ProjectId,dto.Title,dto.TaskDescription,dto.Priority,Ids,dto.FromDate,dto.ToDate));
            await _projectTaskRepo.UnitOfWork.SaveAsync();
            return Ok($"Project task created successfully");
        }
        //view project task
        [HttpGet, Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> ViewProjectTasks()
        {
            int teamLeadId = 0; //should come from the current logged in user
            int.TryParse(User.Identity.Name, out teamLeadId);
            var projectTask = await _projectTaskRepo.GetAll(t => t.TeamLeadId == teamLeadId, "Project");
            var teamMembers = projectTask.Select(t => t.AssignedTo).Split("_");
            string Members = "";
            foreach (var id in teamMembers)
            {
                var tm = await _teamMemberRepo.Get(t => t.Id == id);
                Members += $"{tm.Firstname} {tm.Lastname} -> ";

            }
            return Ok(projectTask.Select(t => new
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
                AssignedTo = Members,
                t.DateCreated
            }));
        }

        //create team members
        [HttpPost, Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> CreateTeamMember([FromBody] CreateTeamMemberDTO dto)
        {
            int teamLeadId = 0; //should come from the current logged in user
            int.TryParse(User.Identity.Name, out teamLeadId);
            await _teamMemberRepo.AddAsync(new TeamMember(dto.CountryId, teamLeadId, dto.Email, dto.FirstName, dto.LastName, dto.Password));
            await _projectTaskRepo.UnitOfWork.SaveAsync();
            return Ok($"Team member created successfully");
        }
        //view team members
        [HttpGet, Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> ViewTeamMembers()
        {
            int teamLeadId = 0; //should come from the current logged in user
            var teamMembers = await _teamMemberRepo.GetAll(t => t.TeamLeadId == teamLeadId);
            if (teamMembers == null) return Problem(detail: "No user found", statusCode: (int)HttpStatusCode.InternalServerError);
            return Ok(teamMembers);
        }

        ////view team member
        //[HttpGet("{Id:int}")]
        //public async Task<IActionResult> ViewTeamMembers(int Id)
        //{
        //    int teamLeadId = 0; //should come from the current logged in user
        //    var teamMember = await _teamMemberRepo.Get(t => t.TeamLeadId == teamLeadId && t.Id == Id);
        //    ArgumentNullException.ThrowIfNull(teamMember); //guard clauses
        //    return Ok(teamMember);
        //}

        //assign team members to project
        [HttpPut("{ProjectId:int}"), Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> AssignTeamMembersToProject(int ProjectId, [FromBody] int teamMemberId)
        {
            int teamLeadId;
            int.TryParse(User.Identity.Name, out teamLeadId);
            var project = await _projectRepo.Get(t => t.TeamLeadId == teamLeadId && t.Id == ProjectId);
            //check if the team member was created by the team lead
            var teamMember = await _teamMemberRepo.Get(t => t.TeamLeadId == teamLeadId && t.Id == teamMemberId);
            if (teamMember == null) return Problem(detail: "Team member does not exists. You can create this user as a team member and add to the project", statusCode: (int)HttpStatusCode.InternalServerError);
            //check if team member is already assigned to project
            if (project.AssignedTeamMemberIds.Split("_").Any(id => id == teamMemberId.ToString()))
            {
                //team member already assigned to project
                return Ok($"Team member ({teamMember.Firstname}) already assigned to project");
            }
            project.AssignTeamMember(teamMemberId); //domain entity behavior

            //save changes to database by caling the UnitOfWork
            await _projectRepo.UnitOfWork.SaveAsync();
            return Ok($"Team member ({teamMember.Firstname}) is now assigned to the project ({project.Name})");


        }

        //define assign team member to project task action method
    }
}
