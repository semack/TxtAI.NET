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
        private readonly string _url;

        public Segmentation(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(url);
        }

        public async Task<object> SegmentAsync(string text)
        {
            var response = await _client.GetAsync($"{_url}/segment?text={text}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        }

        public async Task<List<object>> BatchSegmentAsync(List<string> texts)
        {
            var response = await _client.PostAsJsonAsync($"{_url}/batchsegment", new { texts });
            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<object>>(await response.Content.ReadAsStringAsync());
        }
    }
}