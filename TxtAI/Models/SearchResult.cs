using Newtonsoft.Json;

namespace TxtAI
{
    public class SearchResult
    {
        public SearchResult(string id, double score)
        {
            Id = id;
            Score = score;
        }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("score")] public double Score { get; set; }
    }
}