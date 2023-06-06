using DynamicSun.Api.Models;

namespace DynamicSun.Api.Extensions;

public static class EnumerableExtensions
{
    public static Page<T> AsPage<T>(this IEnumerable<T> values, long total, int offset) where T : notnull
    {
        T[] array = values.ToArray();
        return new Page<T>
        {
            Total = total,
            Offset = offset,
            Count = array.Length,
            Values = (IReadOnlyCollection<T>)(object)array
        };
    }
}