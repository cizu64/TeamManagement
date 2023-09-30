using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.WebAPI.Security;

namespace TaskManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CountryController : ControllerBase
    {
        IGenericRepository<Country> _countryRepository;
        DataProtector protector;
        public CountryController(IGenericRepository<Country> countryRepository, DataProtector protector)
        {
            _countryRepository = countryRepository;
            this.protector = protector;
        }
        [HttpGet]
        [Authorize(Policy = "TeamLeadOnly")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryRepository.GetAll(c => c.IsActive);
            return Ok(countries);
        }
        [HttpGet]
        public async Task<IActionResult> Protector(string text)
        {
            var protect = protector.Protect(text);
            var unprotect = protector.UnProtect(protect);
            return Ok(new { protect, unprotect });
        }
    }
}