using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using RouteProvider.API.Model;
using Route = RouteProvider.API.Model.Route;

namespace RouteProvider.API.Services;

public sealed class CachedService : ICachedService
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _expiration = TimeSpan.FromMinutes(60);

    public CachedService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        InitAllKeysCache();
    }

    public Route? GetRoute(Filter filter)
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
                        return route;
                    }
                }
            }
        }
        else
        {
            InitAllKeysCache();
        }

        return null;
    }

    public void SetRoute(Route? route)
    {
        if (route != null)
        {
            _memoryCache.Set(route.Guid, route,
                new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = _expiration });
            
            if (_memoryCache.TryGetValue(Constants.AllRoutesKeys, out ConcurrentBag<Guid>? collection))
            {
                collection?.Add(route.Guid);
            }
            else
            {
                InitAllKeysCache();
            }
        }
    }
    
    private void InitAllKeysCache()
    {
        _memoryCache.Set(Constants.AllRoutesKeys, new ConcurrentBag<Guid>(), new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _expiration
        });
    }
}