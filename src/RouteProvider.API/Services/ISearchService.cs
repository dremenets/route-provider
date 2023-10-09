using RouteProvider.API.Services.Requests;
using RouteProvider.API.Services.Responses;

namespace RouteProvider.API.Services;

public interface ISearchService
{
    Task<bool> Ping();
    Task<SearchResponse> GetRoute(SearchRequest request);
}