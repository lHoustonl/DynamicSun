namespace DynamicSun.Integration.Client.Http.Models;

public sealed class PageResponse<T>
    where T : class
{
    /// <summary>
    /// Общее количество элементов
    /// </summary>
    public int Total { get; init; }

    /// <summary>
    /// Возвращенное количество элементов
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Массив элементов
    /// </summary>
    public IEnumerable<T> Values { get; init; } = null!;
}