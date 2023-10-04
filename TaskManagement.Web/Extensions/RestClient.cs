using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskManagement.Web.Extensions
{
    /// <summary>
    /// A class for consuming restful services from a .NET client
    /// </summary>
    public static class RestClient
    {
        /// <summary>
        /// Send an http request as an asychronus operation using a named client
        /// </summary>
        /// <param name="httpClientFactory">The IHttpclientFactory Instance</param>
        /// <param name="requestUri">The request uri should be used when the name client is specified</param>
        /// <param name="headers">Specify a dictionary of request headers</param>
        /// <param name="name">The name of the client if it is a named client that would be configured in the Program.cs class.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> SendRequestAsync(this IHttpClientFactory httpClientFactory, HttpMethod method, string requestUri, string name, object? payload=null, IDictionary<string, string>? headers = null)
        {
            var client = httpClientFactory.CreateClient(name);
            if (headers is not null)
            {
                foreach (var items in headers)
                {
                    client.DefaultRequestHeaders.Add(items.Key, items.Value);
                }
            }
            var request = new HttpRequestMessage(method, client.BaseAddress + requestUri);
            var serialize = JsonSerializer.Serialize(payload);
            request.Content = new StringContent(serialize, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.SendAsync(request);
            return response;
        }
        public static async Task<HttpResponseMessage> PostAsync(this IHttpClientFactory httpClientFactory, string requestUri, string name, object? payload = null, IDictionary<string, string>? headers = null)
        {
            var client = httpClientFactory.CreateClient(name);
            if (headers is not null)
            {
                foreach (var items in headers)
                {
                    client.DefaultRequestHeaders.Add(items.Key, items.Value);
                }
            }

            var serialize = JsonSerializer.Serialize(payload);
            var content = new StringContent(serialize, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync(client.BaseAddress + requestUri, content);
            return response;
        }

    }
}
