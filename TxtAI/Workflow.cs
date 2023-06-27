using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI
{
    public class Workflow
    {
        private readonly HttpClient _client;
        private readonly string _url;

        public Workflow(string url)
        {
            _url = url;
            _client = API.Create<HttpClient>(_url);
        }

        public async Task<List<object>> WorkflowActionAsync(string name, List<string> elements)
        {
            var payload = new { name, elements };

            var response = await _client.PostAsJsonAsync($"{_url}/workflow", payload);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<object>>(await response.Content.ReadAsStringAsync());
        }
    }
}