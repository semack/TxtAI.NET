using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TxtAI.Models;

namespace TxtAI
{
    public class Similarity
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Similarity(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(url);
        }

        public async Task<List<IndexResult>> SimilarityAsync(string query, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync($"{_url}/similarity", new { query, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<IndexResult>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<List<IndexResult>>> BatchSimilarityAsync(List<string> queries, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync($"{_url}/batchsimilarity", new { queries, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<List<IndexResult>>>(await response.Content.ReadAsStringAsync());
        }
    }
}