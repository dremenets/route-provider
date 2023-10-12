using System.Text.Json;
using Microsoft.Extensions.Options;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;
using RouteProvider.API.Services;

namespace RouteProvider.API.Providers;

public sealed class ExternalProviderTwo : ExternalProvider, IExternalProviderTwo
{
    private readonly string _url;
    
    public ExternalProviderTwo(IHttpClientFactory httpClientFactory, IOptions<ProviderSettingsConfiguration> options, ICachedService cachedService) :
        base(httpClientFactory, cachedService)
    {
        _url = options.Value.ProviderTwoUrl;
    }

    public Task<bool> Ping() => Ping(_url);

    public async Task<ProviderTwoSearchResponse?> GetRoute(ProviderTwoSearchRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var response = await Search(_url, json);
        var result = await response.Content.ReadFromJsonAsync<ProviderTwoSearchResponse>();

        StoreToCache(result?.Route);

        return result;
    }
}