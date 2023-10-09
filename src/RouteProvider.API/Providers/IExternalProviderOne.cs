using RouteProvider.API.Providers.Requests;
using RouteProvider.API.Providers.Responses;

namespace RouteProvider.API.Providers;

public interface IExternalProviderOne
{
    Task<bool> Ping();
    Task<ProviderOneSearchResponse> GetRoute(ProviderOneSearchRequest request);
}