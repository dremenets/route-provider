using AutoMapper;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;
using RouteProvider.API.Providers;

namespace RouteProvider.API.Services;

public sealed class SearchService : ISearchService
{
    private readonly ILogger<SearchService> _logger;
    private readonly IExternalProviderOne _externalProviderOne;
    private readonly IExternalProviderTwo _externalProviderTwo;
    private readonly IMapper _mapper;

    public SearchService(IExternalProviderOne externalProviderOne,
        IExternalProviderTwo externalProviderTwo,
        ILogger<SearchService> logger, 
        IMapper mapper)
    {
        _externalProviderOne = externalProviderOne;
        _externalProviderTwo = externalProviderTwo;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> Ping()
    {
        try
        {
            var isOneAlive = _externalProviderOne.Ping();
            var isTwoAlive = _externalProviderTwo.Ping();
            var isOk = await isOneAlive &&
                       await isTwoAlive;
            return isOk;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error is during Ping request: {e}");
            return false;
        }
    }

    public async Task<SearchResponse?> GetRoute(SearchRequest request)
    {
        try
        {
            if (request.Filters.OnlyCached)
            {
                // TODO
            }

            var task1 = _externalProviderOne.GetRoute(_mapper.Map<ProviderOneSearchRequest>(request.Filters));
            var task2 = _externalProviderTwo.GetRoute(_mapper.Map<ProviderTwoSearchRequest>(request.Filters));

            await Task.WhenAll(task1, task2);

            var result1 = await task1;
            var result2 = await task2;

            var priceOne = result1?.Route?.Price ?? 0;
            var priceTwo = result2?.Route?.Price ?? 0;

            return new SearchResponse
            {
                Route = priceOne < priceTwo ? result1?.Route : result2?.Route
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error is during Search request: {e}");
            return null;
        }
    }
}