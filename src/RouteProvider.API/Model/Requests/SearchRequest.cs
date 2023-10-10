namespace RouteProvider.API.Model.Requests;

public sealed class SearchRequest
{
    public Filter Filters { get; init; } = new();
}