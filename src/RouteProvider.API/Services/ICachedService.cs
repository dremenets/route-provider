using RouteProvider.API.Model;
using Route = RouteProvider.API.Model.Route;

namespace RouteProvider.API.Services;

public interface ICachedService
{
    Route? GetRoute(Filter request);
    void SetRoute(Route? route);
}