using Newtonsoft.Json;

namespace TxtAI.Models
{
    public class Document
    {
        public Document(string id, string text)
        {
            Id = id;
            Text = text;
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("text")] public string Text { get; set; }
    }
}