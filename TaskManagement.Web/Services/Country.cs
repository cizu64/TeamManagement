using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TaskManagement.Web.Extensions;
namespace TaskManagement.Web.Services
{
    public class Country
    {
        private readonly IHttpClientFactory _client;
        public Country(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<string> Countries()
        {
            //var request =  await _client.SendRequestAsync(HttpMethod.Get,"/getcountries", "Country",null); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            //var countries = await request.Content.ReadAsStringAsync();
            //return countries;
            return "";
        }
    }
}
