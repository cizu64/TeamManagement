using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.WebAPI.DTO;

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

        public TeamMemberController(IGenericRepository<TeamLead> teamLeadRepo, IGenericRepository<Project> projectRepo, IGenericRepository<ProjectTask> projectTaskRepo, IGenericRepository<TeamMember> teamMemberRepo, IGenericRepository<Todo> todoRepo)
        {
            _teamLeadRepo = teamLeadRepo;
            _projectRepo = projectRepo;
            _projectTaskRepo = projectTaskRepo;
            _teamMemberRepo = teamMemberRepo;
            _todoRepo = todoRepo;
        }
        [HttpGet]
        public async Task<IActionResult> ViewProjects()
        {
            int teamMemberId = 0; //current loggedin team member id
            var tm = await _teamMemberRepo.GetAll(t => t.Id == teamMemberId, "TeamLead");
            ArgumentNullException.ThrowIfNull(tm);
            var projects = tm.Select(t => t.TeamLead.Projects);
            return Ok(projects);
        }
        [HttpGet("{projectId:int}")]
        public async Task<IActionResult> ViewProjectTask(int projectId)
        {
            int teamMemberId = 0; //current loggedin team member id
            var pTask = await _projectTaskRepo.GetAll(t => t.Id == projectId);
            ArgumentNullException.ThrowIfNull(pTask);
            var teamMemberIds = pTask.Select(t => t.AssignedTo.Split("_").Where(t => t == teamMemberId.ToString())); //check weather the user is assigned to any project task
            if (teamMemberIds.Any()) //if the user is assigned to the project task, display the project task for the team member
            {
                return Ok(pTask);
            }
            return BadRequest("No project task assigned to you");
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] CreateTodoDTO dto)
        {
            int teamMemberId = 0;
            await _todoRepo.AddAsync(new Todo(teamMemberId, dto.ProjectTaskId, dto.Title, dto.Description));
            await _projectRepo.UnitOfWork.SaveAsync();
            return Ok($"""Todo "({dto.Title})" created successfully"""); //using the new c# raw string literal 
        }

        //view todos
        [HttpGet]
        public async Task<IActionResult> ViewTodo()
        {
            int teamMemberId = 0; //should come from the current logged in user
            var todo = await _todoRepo.Get(t => t.TeamMemberId == teamMemberId);
            ArgumentNullException.ThrowIfNull(todo); //guard clauses
            return Ok(todo);
        }
        //view todo
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> ViewTodos(int Id)
        {
            int teamMemberId = 0; //should come from the current logged in user
            var todo = await _todoRepo.Get(t => t.Id == Id && t.TeamMemberId == teamMemberId);
            ArgumentNullException.ThrowIfNull(todo); //guard clauses
            return Ok(todo);
        }

        //team member sign in
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO dto)
        {
            var teamMember = await _teamMemberRepo.Get(t => t.Email.ToLower() == dto.Email.ToLower() && t.Password == dto.Password);
            ArgumentNullException.ThrowIfNull(teamMember);

            //return jwt token  

            return Ok("token goes here");
        }
    }
}
