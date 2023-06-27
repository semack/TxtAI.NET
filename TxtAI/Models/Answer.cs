using Newtonsoft.Json;

namespace TxtAI
{
    public class Answer
    {
        public Answer(string name, string answerText)
        {
            Name = name;
            AnswerText = answerText;
        }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("answer")] public string AnswerText { get; set; }

        public override string ToString()
        {
            return $"{Name} {AnswerText}";
        }
    }
}