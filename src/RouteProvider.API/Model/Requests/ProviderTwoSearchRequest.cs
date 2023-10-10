namespace RouteProvider.API.Model.Requests;

public sealed class ProviderTwoSearchRequest
{
    public decimal MaxPrice { get; init; }
    public decimal MinPrice { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}