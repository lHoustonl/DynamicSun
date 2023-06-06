using DynamicSun.DataAccess.DataContext.Configurations;
using DynamicSun.Domain;
using Microsoft.EntityFrameworkCore;

namespace DynamicSun.Context;

public class WeatherContext : DbContext
{
    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WeatherArchiveConfiguration());
    }

    public DbSet<WeatherArchive> WeatherArchives { get; set; }
}