using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI
{
    public class Summary
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Summary(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(url);
        }

        public async Task<string> SummaryAsync(string text, int? minLength, int? maxLength)
        {
            var response =
                await _client.GetAsync($"{_url}/summary?text={text}&minLength={minLength}&maxLength={maxLength}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<string>> BatchSummaryAsync(List<string> texts, int? minLength, int? maxLength)
        {
            var payload = new { texts, minLength, maxLength };
            var response = await _client.PostAsJsonAsync($"{_url}/batchsummary", payload);
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}