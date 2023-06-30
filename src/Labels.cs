using TxtAI.NET.Models;

namespace TxtAI.NET;

public class Labels
{
    private readonly HttpClient _client;

    public Labels(string baseUrl, int timeout = 120, string token = null)
    {
        _client = Api.Create<HttpClient>(baseUrl, timeout, token);
    }

    public async Task<List<IndexResult>> LabelAsync(string text, List<string> labels)
    {
        var response = await _client.PostAsJsonAsync("label", new { text, labels });
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadAsAsync<List<IndexResult>>();
    }

    public async Task<List<List<IndexResult>>> BatchLabelAsync(List<string> texts, List<string> labels)
    {
        var response = await _client.PostAsJsonAsync("batchlabel", new { texts, labels });
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadAsAsync<List<List<IndexResult>>>();
    }
}