using System.Text.Json;
using Microsoft.Extensions.Options;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;

namespace RouteProvider.API.Providers;

public sealed class ExternalProviderOne : ExternalProvider, IExternalProviderOne
{
    private readonly string _url;

    public ExternalProviderOne(IHttpClientFactory httpClientFactory, IOptions<ProviderSettingsConfiguration> options)
        : base(httpClientFactory)
    {
        _url = options.Value.ProviderOneUrl;
    }

    public Task<bool> Ping() => Ping(_url);

    public async Task<ProviderOneSearchResponse?> GetRoute(ProviderOneSearchRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        var response = await Search(_url, json);
        return await response.Content.ReadFromJsonAsync<ProviderOneSearchResponse>();
    }
}