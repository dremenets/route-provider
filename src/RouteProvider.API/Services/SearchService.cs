using RouteProvider.API.Providers;
using RouteProvider.API.Services.Requests;
using RouteProvider.API.Services.Responses;

namespace RouteProvider.API.Services;

public sealed class SearchService : ISearchService
{
    private readonly IExternalProviderOne _externalProviderOne;
    private readonly IExternalProviderTwo _externalProviderTwo;

    public SearchService(IExternalProviderOne externalProviderOne, IExternalProviderTwo externalProviderTwo)
    {
        _externalProviderOne = externalProviderOne;
        _externalProviderTwo = externalProviderTwo;
    }

    public async Task<bool> Ping()
    {
        var isOneAlive = _externalProviderOne.Ping();
        var isTwoAlive = _externalProviderTwo.Ping();
        var isOk = await isOneAlive && 
                   await isTwoAlive;
        return isOk;
    }
    

    public Task<SearchResponse> GetRoute(SearchRequest request)
    {
        throw new NotImplementedException();
    }
}