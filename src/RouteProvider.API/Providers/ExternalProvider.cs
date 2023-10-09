using System.Net;

namespace RouteProvider.API.Providers;

public abstract class ExternalProvider
{
    private readonly IHttpClientFactory _httpClientFactory;

    protected ExternalProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected async Task<bool> Ping(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }

        var uri = new Uri(url);

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(new Uri(uri, "/ping"));
        return response.StatusCode == HttpStatusCode.OK;
    }
}