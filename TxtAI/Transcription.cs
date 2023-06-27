using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI
{
    public class Transcription
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Transcription(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(_url);
        }

        public async Task<string> TranscribeAsync(string file)
        {
            var response = await _client.GetAsync($"{_url}/transcribe?file={file}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<string>> BatchTranscribeAsync(List<string> files)
        {
            var payload = new { files };

            var response = await _client.PostAsJsonAsync($"{_url}/batchtranscribe", payload);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}