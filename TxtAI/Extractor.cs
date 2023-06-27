using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace txtai
{
    public class Extractor
    {
        private string url;
        private HttpClient api;

        public class Question
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("query")]
            public string Query { get; set; }

            [JsonProperty("question")]
            public string QuestionText { get; set; }

            [JsonProperty("snippet")]
            public bool Snippet { get; set; }

            public Question(string name, string query, string questionText, bool snippet)
            {
                Name = name;
                Query = query;
                QuestionText = questionText;
                Snippet = snippet;
            }
        }

        public class Answer
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("answer")]
            public string AnswerText { get; set; }

            public Answer(string name, string answerText)
            {
                Name = name;
                AnswerText = answerText;
            }

            public override string ToString()
            {
                return $"{Name} {AnswerText}";
            }
        }

        public Extractor(string url)
        {
            this.url = url;
            this.api = API.Create<HttpClient>(url);
        }

        public async Task<List<Answer>> Extract(List<Question> queue, List<string> texts)
        {
            var response = await this.api.PostAsJsonAsync($"{this.url}/extract", new { queue, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<Answer>>();
        }
    }
}