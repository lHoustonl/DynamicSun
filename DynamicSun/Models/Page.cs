namespace DynamicSun.Api.Models;

public sealed class Page<T> where T : notnull
{
    public long Total { get; internal set; }

    public int Offset { get; internal set; }

    public int Count { get; internal set; }

    public IReadOnlyCollection<T> Values { get; set; }

    public Page()
    {
        Values = (IReadOnlyCollection<T>)(object)Array.Empty<T>();
    }

    public static Page<T> Empty(int offset)
    {
        return new Page<T>
        {
            Total = 0L,
            Offset = offset,
            Count = 0,
            Values = (IReadOnlyCollection<T>)(object)Array.Empty<T>()
        };
    }
}