using System.Net.Http.Headers;
using System.Text.Json;

namespace StudentCenterWeb.Util;

public static class HttpClientExtensions
{
    private static MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("application/json");

    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw new ApplicationException($"Something went wrong calling the API: " +
                                                                          $"{response.ReasonPhrase}");

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        ?? throw new ApplicationException("Couldn't deserialize the response");
    }

    public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);

        var content = new StringContent(dataAsString);

        content.Headers.ContentType = _contentType;

        return httpClient.PostAsync(url, content);
    }
}
