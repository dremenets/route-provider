using System.Text.Json;
using Microsoft.Extensions.Options;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;

namespace RouteProvider.API.Providers;

public sealed class ExternalProviderTwo : ExternalProvider, IExternalProviderTwo
{
    private readonly string _url;
    
    public ExternalProviderTwo(IHttpClientFactory httpClientFactory, IOptions<ProviderSettingsConfiguration> options) :
        base(httpClientFactory)
    {
        _url = options.Value.ProviderTwoUrl;
    }

    public Task<bool> Ping() => Ping(_url);

    public async Task<ProviderTwoSearchResponse?> GetRoute(ProviderTwoSearchRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var response = await Search(_url, json);
        return await response.Content.ReadFromJsonAsync<ProviderTwoSearchResponse>();
    }
}