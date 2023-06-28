using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TxtAI
{
    class Api
    {
        public static T Create<T>(string baseUrl, int timeout = 120, string token = null) where T : HttpClient, new()
        {
            var client = new T();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(timeout);
            if (string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            client.BaseAddress = new Uri(baseUrl);

            return client;
        }
    }
}