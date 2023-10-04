namespace TaskManagement.Web.Common
{
    public class APIResult
    {
        public string type { get; set; }
        public string title { get; set; }
        public int statusCode { get; set; }
        public string detail { get; set; }
        public string traceId { get; set; }

    }
}
