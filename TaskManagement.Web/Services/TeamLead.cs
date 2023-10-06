using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using TaskManagement.Web.VM;
using TaskManagement.Web.Common;
using TaskManagement.Web.Extensions;
namespace TaskManagement.Web.Services
{
    public class TeamLead
    {
        private readonly IHttpClientFactory _client;
        public TeamLead(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<APIResult> Login(string email, string password)
        {
            var data = new
            {
                email,
                password
            };
            var request = await _client.SendRequestAsync(HttpMethod.Post,"/Signin", "TeamLead", data, null); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var token = await request.Content.ReadFromJsonAsync<string>();
                return new APIResult
                {
                    detail= token,
                    statusCode = (int)request.StatusCode
                };
            }
            var errorDetails = await request.Content.ReadAsStringAsync();
            var result = errorDetails == "" ? new APIResult() : JsonSerializer.Deserialize<APIResult>(errorDetails);
            return result;
        }

        public async Task<APIResult> Register(int countryId, string email, string firstname,string lastname, string password)
        {
            var data = new
            {
                email,
                firstname,
                lastname,
                countryId,
                password
            };
            var request = await _client.SendRequestAsync(HttpMethod.Post, "/CreateAccount", "TeamLead", data, null); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var token = await request.Content.ReadAsStringAsync();
                return new APIResult
                {
                    detail = token,
                    statusCode = (int)request.StatusCode
                };
            }
            var errorDetails = await request.Content.ReadAsStringAsync();
            var result = errorDetails == "" ? new APIResult() : JsonSerializer.Deserialize<APIResult>(errorDetails);
            return result;
        }

        public async Task<APIResult> AddProject(string token,string name, string description,string[] assignedTeamMemberIds)
        {
            
            var data = new
            {
                name,
                description,
                assignedTeamMemberIds,
            };
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            var request = await _client.SendRequestAsync(HttpMethod.Post, "/CreateProject", "TeamLead", data, headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var message = await request.Content.ReadAsStringAsync();
                return new APIResult
                {
                    detail = message,
                    statusCode = (int)request.StatusCode
                };
            }
            var errorDetails = await request.Content.ReadAsStringAsync();
            var result = errorDetails == "" ? new APIResult() : JsonSerializer.Deserialize<APIResult>(errorDetails);
            return result;
        }

        public async Task<APIResult> ViewProject(string token, int projectId)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ViewProject/{projectId}", "TeamLead", null , headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var project = await request.Content.ReadFromJsonAsync<ViewProject>();
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

        public async Task<APIResult> ViewProjects(string token)
        {
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ViewProjects", "TeamLead", null, headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
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

        public async Task<APIResult> AddTask(string token,int projectId, string[] assignedTo, string title, string description, string priority, string from, string to)
        {
            var data = new
            {
                FromDate = from,
                assignedTo,
                ToDate = to,
                priority,
                projectId,
                title,
                TaskDescription = description
            };
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };

            var request = await _client.SendRequestAsync(HttpMethod.Post, "/CreateProjectTask", "TeamLead", data, headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
            if (request.IsSuccessStatusCode)
            {
                var message = await request.Content.ReadAsStringAsync();
                return new APIResult
                {
                    detail = message,
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

            var request = await _client.SendRequestAsync(HttpMethod.Get, $"/ViewProjects", "TeamLead", null, headers); //the base url is null because we are using a named instance that defines the base url in the program.cs class
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

    }
}
