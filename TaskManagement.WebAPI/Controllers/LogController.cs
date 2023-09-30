using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private readonly IGenericRepository<Log> _log;

        public LogController(IGenericRepository<Log> log)
        {
            _log = log;
        }
        [HttpGet]
        public async Task<IActionResult> ViewLogs()
        {
            var logs = await _log.GetAll();
            return Ok(logs);
        }
        [HttpGet]
        public IActionResult PaginateCursor(int cursorId=0, int pageSize = 10)
        {
            var a = Enumerable.Range(1, 500);
            var result = a.Where(n => n > cursorId).Take(pageSize);
            return Ok(result);
        }
    }
}
