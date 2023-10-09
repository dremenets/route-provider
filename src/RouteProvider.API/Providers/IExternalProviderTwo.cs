using RouteProvider.API.Providers.Requests;
using RouteProvider.API.Providers.Responses;

namespace RouteProvider.API.Providers;

public interface IExternalProviderTwo
{
    Task<bool> Ping();
    Task<ProviderTwoSearchResponse> GetRoute(ProviderTwoSearchRequest request);
}