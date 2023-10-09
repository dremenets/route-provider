using Microsoft.Extensions.Options;
using RouteProvider.API.Providers.Requests;
using RouteProvider.API.Providers.Responses;

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

    public Task<ProviderTwoSearchResponse> GetRoute(ProviderTwoSearchRequest request)
    {
        throw new NotImplementedException();
    }
}