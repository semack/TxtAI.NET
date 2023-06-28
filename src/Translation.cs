using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI.NET
{
    public class Translation
    {
        private readonly HttpClient _client;

        public Translation(string baseUrl, int timeout = 120, string token = null)
        {
            _client = Api.Create<HttpClient>(baseUrl, timeout, token);
        }

        public async Task<string> TranslateAsync(string text, string target, string source)
        {
            var response = await _client.GetAsync("translate?text={text}&target={target}&source={source}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<string>> BatchTranslateAsync(List<string> texts, string target, string source)
        {
            var payload = new { texts, target, source };

            var response = await _client.PostAsJsonAsync("batchtranslate", payload);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}