using Newtonsoft.Json;

namespace TxtAI.NET;

public class Segmentation
{
    private readonly HttpClient _client;

    public Segmentation(string baseUrl, int timeout = 120, string token = null)
    {
        _client = Api.Create<HttpClient>(baseUrl, timeout, token);
    }

    public async Task<string> SegmentAsync(string text)
    {
        var response = await _client.GetAsync($"segment?text={text}");
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<List<string>> BatchSegmentAsync(List<string> texts)
    {
        var response = await _client.PostAsJsonAsync("batchsegment", new { texts });
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
    }
}