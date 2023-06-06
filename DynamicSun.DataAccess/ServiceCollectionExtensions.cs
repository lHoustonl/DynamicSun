using DynamicSun.Context;
using DynamicSun.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicSun.DataAccess;

public static class ServiceCollectionExtensions
{
    public static void AddPostgresSqlDataAccessSchemaMigrator(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<DataAccessSchemaMigratorDbContext>(builder =>
            builder.UseNpgsql(connectionString));
        collection.AddScoped<IDataAccessSchemaMigrator, DataAccessSchemaMigrator>();
    }

    public static void AddWeatherRepositories(this IServiceCollection collection, string connectionString)
    {
        collection.AddPostgresSqlRepositories<IWeatherArchiveRepository, WeatherArchiveRepository, WeatherContext>(connectionString);
    }

    private static void AddPostgresSqlRepositories<TRepository, TRepositoryImplementation, TRepositoryDbContext>(
        this IServiceCollection collection, string connectionString)
        where TRepository : class
        where TRepositoryImplementation : class, TRepository
        where TRepositoryDbContext : DbContext
    {
        collection.AddDbContextFactory<TRepositoryDbContext>(builder =>
            builder.UseNpgsql(connectionString)
        );

        collection.AddScoped<TRepository, TRepositoryImplementation>();
    }
}