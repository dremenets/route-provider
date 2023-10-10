using System.Collections.Concurrent;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using RouteProvider.API.Model;
using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;
using RouteProvider.API.Providers;
using Route = RouteProvider.API.Model.Route;

namespace RouteProvider.API.Services;

public sealed class SearchService : ISearchService
{
    private readonly ILogger<SearchService> _logger;
    private readonly IExternalProviderOne _externalProviderOne;
    private readonly IExternalProviderTwo _externalProviderTwo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;

    public SearchService(IExternalProviderOne externalProviderOne,
        IExternalProviderTwo externalProviderTwo,
        ILogger<SearchService> logger, 
        IMapper mapper, 
        IMemoryCache memoryCache)
    {
        _externalProviderOne = externalProviderOne;
        _externalProviderTwo = externalProviderTwo;
        _logger = logger;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    public async Task<bool> Ping()
    {
        try
        {
            _logger.LogInformation("Starting ping request.");
            
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
        if (request.Filters.StartDate > request.Filters.EndDate)
        {
            throw new InvalidDataException($"{nameof(request.Filters.StartDate)} must be less than {nameof(request.Filters.EndDate)}");
        }
        if (request.Filters.MinPrice > request.Filters.MaxPrice)
        {
            throw new InvalidDataException($"{nameof(request.Filters.MinPrice)} must be less than {nameof(request.Filters.MinPrice)}");
        }

        try
        {
            _logger.LogInformation("Starting search request.");
            
            if (request.Filters.OnlyCached)
            {
                return GetCachedRoute(request.Filters);
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
            throw new InvalidOperationException("Failed to execute search");
        }
    }

    private SearchResponse? GetCachedRoute(Filter filter)
    {
        if (_memoryCache.TryGetValue(Constants.AllRoutesKeys, out ConcurrentBag<Guid>? collection))
        {
            foreach (var item in collection!.Distinct())
            {
                if (_memoryCache.TryGetValue(item, out Route? route))
                {
                    if (route?.StartAt >= filter.StartDate && route.EndAt <= filter.EndDate &&
                        route.Price >= filter.MinPrice && route.Price <= filter.MaxPrice)
                    {
                        return new SearchResponse
                        {
                            Route = route
                        };
                    }
                }
            }
        }

        return null;
    }
}