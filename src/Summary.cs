using Newtonsoft.Json;

namespace TxtAI.NET;

public class Summary
{
    private readonly HttpClient _client;

    public Summary(string baseUrl, int timeout = 120, string token = null)
    {
        _client = Api.Create<HttpClient>(baseUrl, timeout, token);
    }

    public async Task<string> SummarizeAsync(string text, int? minLength, int? maxLength)
    {
        var response =
            await _client.GetAsync($"summary?text={text}&minLength={minLength}&maxLength={maxLength}");
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<List<string>> BatchSummarizeAsync(List<string> texts, int? minLength, int? maxLength)
    {
        var payload = new { texts, minLength, maxLength };
        var response = await _client.PostAsJsonAsync("batchsummary", payload);
        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
    }
}