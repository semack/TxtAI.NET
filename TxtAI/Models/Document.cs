using Newtonsoft.Json;

namespace TxtAI.Models
{
    public class Document
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("text")] public string Text { get; set; }
    }
}