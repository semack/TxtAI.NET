using Newtonsoft.Json;

namespace TxtAI.NET.Models
{
    public class IndexResult
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("score")] public string Score { get; set; }
    }
}