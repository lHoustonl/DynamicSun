using DynamicSun.Integration.Client.Http.Models;
using Refit;

namespace DynamicSun.Integration.Client.Http;

public interface IDynamicSunClient
{
    /// <summary>
    /// Get paged weather archive list
    /// </summary>
    /// <returns>
    /// 200 - ok
    /// </returns>
    [Get("/api/Weather")]
    Task<ApiResponse<PageResponse<GetPagedWeatherArchiveListItem>>> GetPagedWeatherArchiveListAsync(
        GetPagedAgentsListRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get weather archive years
    /// </summary>
    /// <returns>
    /// 200 - ok
    /// </returns>
    [Get("/api/Weather/Years")]
    Task<ApiResponse<IEnumerable<int>>> GetWeatherArchiveYearsAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Upload weather archive
    /// </summary>
    /// <returns>
    /// 200 - ok
    /// </returns>
    [Multipart]
    [Post("/api/Weather")]
    Task<IApiResponse> UploadWeatherArchiveAsync(
        [AliasAs("file")] StreamPart request, CancellationToken cancellationToken = default);
}