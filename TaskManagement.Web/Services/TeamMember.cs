using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TaskManagement.Web.VM;
using TaskManagement.Web.Common;
using TaskManagement.Web.Extensions;
using static TaskManagement.Web.VM.TeamMemberVM;

namespace TaskManagement.Web.Services
{
    public class TeamMember
    {
        private readonly IHttpClientFactory _client;
        public TeamMember(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<APIResult> ViewProjects(string token)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ViewProjects", "TeamMember", null, headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var project = await request.Content.ReadFromJsonAsync<IReadOnlyList<AllProject>>();
                return new APIResult
                {
                    detail = project,
                    statusCode = (int)request.StatusCode
                };
            }

            var errorDetails = await request.Content.ReadAsStringAsync();
            var result = errorDetails == "" ? new APIResult() : JsonSerializer.Deserialize<APIResult>(errorDetails);
            return result;
        }

        public async Task<APIResult> ViewTasks(string token)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ViewProjectTask", "TeamMember", null, headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var tasks = await request.Content.ReadFromJsonAsync<ProjectTask[]>();
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