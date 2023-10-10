namespace RouteProvider.API.Model;

public record Filter
{
    public decimal MaxPrice { get; init; }
    public decimal MinPrice { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool OnlyCached { get; init; }
}