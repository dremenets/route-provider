using System.Text.Json;
using Microsoft.Extensions.Options;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;
using RouteProvider.API.Services;

namespace RouteProvider.API.Providers;

public sealed class ExternalProviderOne : ExternalProvider, IExternalProviderOne
{
    private readonly string _url;
    

    public ExternalProviderOne(IHttpClientFactory httpClientFactory, IOptions<ProviderSettingsConfiguration> options,
        ICachedService cachedService)
        : base(httpClientFactory, cachedService)
    {
        _url = options.Value.ProviderOneUrl;
    }

    public Task<bool> Ping() => Ping(_url);

    public async Task<ProviderOneSearchResponse?> GetRoute(ProviderOneSearchRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var response = await Search(_url, json);
        var result = await response.Content.ReadFromJsonAsync<ProviderOneSearchResponse>();

        StoreToCache(result?.Route);

        return result;
    }
}