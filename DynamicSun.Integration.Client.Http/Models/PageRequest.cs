namespace DynamicSun.Integration.Client.Http.Models;

public abstract class PageRequest
{
    public int Offset { get; init; }
    public int Count { get; init; }
}