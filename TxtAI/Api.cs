using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TxtAI
{
    public class API
    {
        private readonly static HttpClient _httpClient = new HttpClient();

        static API()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient.Timeout = TimeSpan.FromMinutes(2);

            // Interceptor like functionality
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Token");
        }

        public static T Create<T>(string url) where T : class
        {
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var service = Activator.CreateInstance(typeof(T), _httpClient) as T;

            return service;
        }
    }
}