using DynamicSun.Domain;

namespace DynamicSun.Application.Services.WeatherService;

public interface IWeatherService
{
    Task<IEnumerable<WeatherArchive>> GetWeatherArchiveListAsync(int offset, int count, int year, int month, CancellationToken cancellationToken);
    Task<IEnumerable<int>> GetWeatherArchiveYearsAsync(CancellationToken cancellationToken);
    Task UploadArchivesAsync(IEnumerable<WeatherArchive> archives, CancellationToken cancellationToken = default);
    Task<long> CountAsync(int year, int month, CancellationToken cancellationToken);
}