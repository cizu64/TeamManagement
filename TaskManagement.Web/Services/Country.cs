using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TaskManagement.Web.Common;
using TaskManagement.Web.Extensions;
using TaskManagement.Web.VM;

namespace TaskManagement.Web.Services
{
    public class Country
    {
        private readonly IHttpClientFactory _client;
        public Country(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<APIResult> Countries()
        {
            var request =  await _client.SendRequestAsync(HttpMethod.Get,"/GetCountries", "Country",null); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var tasks = await request.Content.ReadFromJsonAsync<Country[]>();
                return new APIResult
                {
                    detail = tasks,
                    statusCode = (int)request.StatusCode
                };
            }
            var errorDetails = await request.Content.ReadAsStringAsync();
            var result = errorDetails == "" ? new APIResult() : JsonSerializer.Deserialize<APIResult>(errorDetails);
            return result;
        }
    }
}
