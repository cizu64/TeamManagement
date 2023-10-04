using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.WebAPI.Security;

namespace TaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ValidateTokenController : ControllerBase
    {
        private readonly JwtAuth auth;
        public ValidateTokenController(JwtAuth auth)
        {
            this.auth = auth;
        }
        [HttpGet]
        public async Task<IActionResult> ValidateToken(string token)
        {
            var validationResult = await auth.ValidateToken(token);
            if(validationResult.Item1 == null) { return BadRequest(new { Role = "", Isvalid = false }); }
            var role = validationResult.Item1.FindFirst(x => x.Type == ClaimTypes.Role);
            //var usid = validationResult.Item1.FindFirst(x => x.Type == ClaimTypes.Name);
            return Ok(new { Role = role.Value, Isvalid = validationResult.Item2 });
        }
    }
}
