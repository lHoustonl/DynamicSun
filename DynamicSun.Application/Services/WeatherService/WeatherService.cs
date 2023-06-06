using DynamicSun.DataAccess.Repositories;
using DynamicSun.Domain;

namespace DynamicSun.Application.Services.WeatherService;

public class WeatherService : IWeatherService
{
    private readonly IWeatherArchiveRepository _weatherArchiveRepository;

    public WeatherService(IWeatherArchiveRepository weatherArchiveRepository)
    {
        _weatherArchiveRepository = weatherArchiveRepository;
    }

    public async Task<long> CountAsync(int year, int month, CancellationToken cancellationToken)
        => await _weatherArchiveRepository.CountAsync(year, month, cancellationToken);

    public async Task<IEnumerable<WeatherArchive>> GetWeatherArchiveListAsync(int offset, int count, int year, int month, CancellationToken cancellationToken)
        => await _weatherArchiveRepository.GetWeatherArchiveListAsync(offset, count, year, month, cancellationToken);

    public async Task<IEnumerable<int>> GetWeatherArchiveYearsAsync(CancellationToken cancellationToken)
        => await _weatherArchiveRepository.GetWeatherArchiveYearsAsync(cancellationToken);

    public async Task UploadArchivesAsync(IEnumerable<WeatherArchive> archives, CancellationToken cancellationToken = default)
        => await _weatherArchiveRepository.UploadArchivesAsync(archives, cancellationToken);
}