using System.Text.Json;

namespace WorkflowApi.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
