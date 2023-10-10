using RouteProvider.API.Model.Requests;
using RouteProvider.API.Model.Responses;

namespace RouteProvider.API.Services;

public interface ISearchService
{
    Task<bool> Ping();
    Task<SearchResponse?> GetRoute(SearchRequest request);
}