using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TxtAI.Models;

namespace TxtAI
{
    public class Labels
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Labels(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(url);
        }

        public async Task<List<IndexResult>> LabelAsync(string text, List<string> labels)
        {
            var response = await _client.PostAsJsonAsync($"{_url}/label", new { text, labels });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<IndexResult>>();
        }

        public async Task<List<List<IndexResult>>> BatchLabelAsync(List<string> texts, List<string> labels)
        {
            var response = await _client.PostAsJsonAsync($"{_url}/batchlabel", new { texts, labels });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<List<IndexResult>>>();
        }
    }
}