using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
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
        private readonly IMemoryCache _memoryCache;

        public CountryController(IGenericRepository<Country> countryRepository, IMemoryCache memoryCache)
        {
            _countryRepository = countryRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        //[EnableRateLimiting("fixed")]
        [ResponseCache(Duration=1800,Location =ResponseCacheLocation.Any)] //cache for 30 minutes
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _countryRepository.GetAll(c => c.IsActive);
            return Ok(countries);
        }
       
    }
}