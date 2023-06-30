using TxtAI.NET.Models;

namespace TxtAI.NET;

public class Extractor
{
    private readonly HttpClient _client;

    public Extractor(string baseUrl, int timeout = 120, string token = null)
    {
        _client = Api.Create<HttpClient>(baseUrl, timeout, token);
    }

    public async Task<List<Answer>> ExtractAsync(List<Question> queue, List<string> texts)
    {
        var response = await _client.PostAsJsonAsync("extract", new { queue, texts });
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadAsAsync<List<Answer>>();
    }
}