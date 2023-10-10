using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;

namespace RouteProvider.API.Providers;

public interface IExternalProviderTwo
{
    Task<bool> Ping();
    Task<ProviderTwoSearchResponse?> GetRoute(ProviderTwoSearchRequest request);
}