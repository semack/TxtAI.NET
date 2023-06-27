using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TxtAI.Models;

namespace TxtAI
{
    public class Extractor
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Extractor(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(url);
        }

        public async Task<List<Answer>> ExtractAsync(List<Question> queue, List<string> texts)
        {
            var response = await _client.PostAsJsonAsync($"{_url}/extract", new { queue, texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsAsync<List<Answer>>();
        }
    }
}