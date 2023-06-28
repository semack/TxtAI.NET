using Newtonsoft.Json;

namespace TxtAI.NET.Models
{
    public class SearchResult
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("score")] public double Score { get; set; }
    }
}