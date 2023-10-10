using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;

namespace RouteProvider.API.Providers;

public interface IExternalProviderOne
{
    Task<bool> Ping();
    Task<ProviderOneSearchResponse?> GetRoute(ProviderOneSearchRequest request);
}