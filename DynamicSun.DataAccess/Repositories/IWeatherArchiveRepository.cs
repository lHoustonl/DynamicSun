using DynamicSun.Domain;

namespace DynamicSun.DataAccess.Repositories;

public interface IWeatherArchiveRepository
{
    Task<IEnumerable<WeatherArchive>> GetWeatherArchiveListAsync(int offset, int count, int year, int month, CancellationToken cancellationToken);
    Task<IEnumerable<int>> GetWeatherArchiveYearsAsync(CancellationToken cancellationToken);
    Task UploadArchivesAsync(IEnumerable<WeatherArchive> archives, CancellationToken cancellationToken);
    Task<long> CountAsync(int year, int month, CancellationToken cancellationToken);
}