using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TxtAI.NET.Models;

namespace TxtAI.NET
{
    public class Embeddings
    {
        private readonly HttpClient _client;

        public Embeddings(string baseUrl, int timeout = 120, string token = null)
        {
            _client = Api.Create<HttpClient>(baseUrl, timeout, token);
        }

        public async Task<List<SearchResult>> SearchAsync(string query, int limit)
        {
            var response = await _client.GetAsync($"search?query={query}&limit={limit}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<SearchResult>>();
        }

        public async Task<List<List<SearchResult>>> BatchSearchAsync(List<string> queries, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("batchsearch", new { queries, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<List<SearchResult>>>();
        }

        public async Task AddAsync(List<Document> documents)
        {
            var response = await _client.PostAsJsonAsync("add", documents);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task IndexAsync()
        {
            var response = await _client.GetAsync("index");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task UpsertAsync()
        {
            var response = await _client.GetAsync("upsert");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<string>> DeleteAsync(List<string> ids)
        {
            var response = await _client.PostAsJsonAsync("delete", ids);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<string>>();
        }

        public async Task<int> CountAsync()
        {
            var response = await _client.GetAsync("count");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<int>();
        }

        public async Task<List<IndexResult>> SimilarityAsync(string query, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("similarity", new { query, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<IndexResult>>();
        }

        public async Task<List<List<IndexResult>>> BatchSimilarityAsync(List<string> queries, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("batchsimilarity", new { queries, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<List<IndexResult>>>();
        }

        public async Task<List<double>> TransformAsync(string text)
        {
            var response = await _client.GetAsync("transform?text={text}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<double>>();
        }

        public async Task<List<List<double>>> BatchTransformAsync(List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("batchtransform", texts);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<List<double>>>();
        }
    }
}