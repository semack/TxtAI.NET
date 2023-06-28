using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TxtAI.NET
{
    public class Workflow
    {
        private readonly HttpClient _client;

        public Workflow(string baseUrl, int timeout = 120, string token = null)
        {
            _client = Api.Create<HttpClient>(baseUrl, timeout, token);
        }

        public async Task<List<object>> WorkflowActionAsync(string name, List<string> elements)
        {
            var payload = new { name, elements };

            var response = await _client.PostAsJsonAsync("workflow", payload);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return JsonConvert.DeserializeObject<List<object>>(await response.Content.ReadAsStringAsync());
        }
    }
}