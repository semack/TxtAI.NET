using Newtonsoft.Json;

namespace TxtAI.Models
{
    public class Answer
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("answer")] public string AnswerText { get; set; }

        public override string ToString()
        {
            return $"{Name} {AnswerText}";
        }
    }
}