using Microsoft.Extensions.Options;
using RouteProvider.API.Providers.Requests;
using RouteProvider.API.Providers.Responses;

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

    public Task<ProviderOneSearchResponse> GetRoute(ProviderOneSearchRequest request)
    {
        throw new NotImplementedException();
    }
}