using System.Collections.Concurrent;
using System.Net;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Route = RouteProvider.API.Model.Route;

namespace RouteProvider.API.Providers;

public abstract class ExternalProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;

    protected ExternalProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
    {
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
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

    protected void StoreToCache(Route? route)
    {
        if (route != null)
        {
            _memoryCache.Set(route.Guid, route);
            if (_memoryCache.TryGetValue(Constants.AllRoutesKeys, out ConcurrentBag<Guid>? collection))
            {
                collection?.Add(route.Guid);
            }
        }
    }

    private static void ThrowIfNullOrEmpty(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url));
        }
    }
}