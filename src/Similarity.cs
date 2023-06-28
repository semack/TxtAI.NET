using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TxtAI.NET.Models;

namespace TxtAI.NET
{
    public class Similarity
    {
        private readonly HttpClient _client;

        public Similarity(string baseUrl, int timeout = 120, string token = null)
        {
            _client = Api.Create<HttpClient>(baseUrl, timeout, token);
        }

        public async Task<List<IndexResult>> SimilarityAsync(string query, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("similarity", new { query, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<IndexResult>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<List<IndexResult>>> BatchSimilarityAsync(List<string> queries, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("batchsimilarity", new { queries, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<List<IndexResult>>>(await response.Content.ReadAsStringAsync());
        }
    }
}