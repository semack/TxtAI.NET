using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI
{
    public class Segmentation
    {
        private readonly HttpClient _client;

        public Segmentation(string baseUrl, int timeout = 120, string token = null)
        {
            _client = Api.Create<HttpClient>(baseUrl, timeout, token);
        }

        public async Task<object> SegmentAsync(string text)
        {
            var response = await _client.GetAsync("segment?text={text}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<object>> BatchSegmentAsync(List<string> texts)
        {
            var response = await _client.PostAsJsonAsync("batchsegment", new { texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<object>>(await response.Content.ReadAsStringAsync());
        }
    }
}