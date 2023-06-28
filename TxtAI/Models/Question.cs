using Newtonsoft.Json;

namespace TxtAI.Models
{
    public class Question
    {

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("query")] public string Query { get; set; }

        [JsonProperty("question")] public string QuestionText { get; set; }

        [JsonProperty("snippet")] public bool Snippet { get; set; }
    }
}