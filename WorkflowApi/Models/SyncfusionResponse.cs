using System.Text.Json.Serialization;

namespace WorkflowApi.Models
{
    public class SyncfusionResponse<T>
    {
        [JsonPropertyName("Items")]
        public List<T>? Items { get; set; }

        [JsonPropertyName("Count")]
        public int Count { get; set; }

    }
}
