using System.Net;
using System.Net.Mime;
using System.Text;

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
        ThrowIfNullOrEmpty(url);

        var uri = new Uri(url);

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(new Uri(uri, "/ping"));
        return response.StatusCode == HttpStatusCode.OK;
    }

    protected async Task<HttpResponseMessage> Search(string url, string json)
    {
        ThrowIfNullOrEmpty(url);

        var uri = new Uri(new Uri(url), "/search").ToString();
        var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var client = _httpClientFactory.CreateClient();
        return await client.PostAsync(uri, content);
    }
    
    private static void ThrowIfNullOrEmpty(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }
    }
}