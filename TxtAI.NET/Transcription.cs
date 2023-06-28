using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI.NET
{
    public class Transcription
    {
        private readonly HttpClient _client;

        public Transcription(string baseUrl, int timeout = 120, string token = null)
        {
            _client = Api.Create<HttpClient>(baseUrl, timeout, token);
        }

        public async Task<string> TranscribeAsync(string file)
        {
            var response = await _client.GetAsync("transcribe?file={file}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<string>> BatchTranscribeAsync(List<string> files)
        {
            var payload = new { files };

            var response = await _client.PostAsJsonAsync("batchtranscribe", payload);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
        }
    }
}