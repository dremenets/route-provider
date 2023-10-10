namespace RouteProvider.API.Model;

public record Route
{
    public Guid Guid { get; init; }
    public string StartPoint { get; init; } = string.Empty;
    public string FinishPoint { get; init; } = string.Empty;
    public DateTime StartAt { get; init; }
    public DateTime EndAt { get; init; }
    public decimal Price { get; init; }
    public int Ttl { get; init; }
}