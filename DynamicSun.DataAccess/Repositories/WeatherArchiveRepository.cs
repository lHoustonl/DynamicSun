using DynamicSun.Context;
using DynamicSun.Domain;
using Microsoft.EntityFrameworkCore;

namespace DynamicSun.DataAccess.Repositories;

public class WeatherArchiveRepository : IWeatherArchiveRepository
{
    private readonly WeatherContext _context;

    public WeatherArchiveRepository(WeatherContext context)
        => _context = context;

    public async Task<long> CountAsync(
        int year, int month,
        CancellationToken cancellationToken)
    {
        IQueryable<WeatherArchive> query = _context.WeatherArchives;

        if (year != 0)
            query = query.Where(w => w.Date.Year == year);

        if (month != 0)
            query = query.Where(w => w.Date.Month == month);

        return await query.CountAsync(cancellationToken);
    }

    public async Task<IEnumerable<WeatherArchive>> GetWeatherArchiveListAsync(
        int offset, int count, int year, int month,
        CancellationToken cancellationToken)
    {
        IQueryable<WeatherArchive> query = _context.WeatherArchives.OrderBy(x => x.Date)
                                                                   .ThenBy(x => x.Time);

        if (year != 0)
            query = query.Where(w => w.Date.Year == year);

        if (month != 0)
            query = query.Where(w => w.Date.Month == month);

        return await query.Skip(offset)
                          .Take(count)
                          .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<int>> GetWeatherArchiveYearsAsync(
        CancellationToken cancellationToken)
        => await _context.WeatherArchives
                         .Select(w => w.Date.Year)
                         .Distinct()
                         .OrderBy(w => w)
                         .ToListAsync(cancellationToken);

    public async Task UploadArchivesAsync(
        IEnumerable<WeatherArchive> archives,
        CancellationToken cancellationToken)
    {
        _context.WeatherArchives.AddRange(archives);
        await _context.SaveChangesAsync(cancellationToken);
    }
}