
namespace TaskManagement.Web.Common
{
    public class APIResult
    {
        public string type { get; set; }
        public string title { get; set; }
        public int statusCode { get; set; }
        public Errors errors { get; set; } 
        public object detail { get; set; }
        public string traceId { get; set; }
        public object data { get; set; }
    }
    public class Errors
    {
        public string[] Name { get; set; }
    }
}
