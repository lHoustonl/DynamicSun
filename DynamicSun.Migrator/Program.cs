﻿using Serilog;
using System.Net.Sockets;
using Polly;
using DynamicSun.DataAccess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
    .ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection")!;
        services.AddPostgresSqlDataAccessSchemaMigrator(connectionString);
    })
    .Build();

var scope = host.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<IDataAccessSchemaMigrator>>();
var dataAccessSchemaMigrator = scope.ServiceProvider.GetRequiredService<IDataAccessSchemaMigrator>();

logger.LogInformation("Migrator - started");

try
{
    await Policy
        .HandleInner<SocketException>()
        .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt * 0.5)),
            (exception, retryCount, timeSpan) => logger.LogWarning(
                "Attempt {retryCount} to connect to pgsql failed. Retry after {timeSpan}. {message}",
                retryCount, timeSpan, exception.Message))
        .ExecuteAsync(async () => { await dataAccessSchemaMigrator.MigrateAsync(); });
}
catch (Exception e)
{
    logger.LogError(e, "Migration failed");
    throw;
}

logger.LogInformation("Migrator - stopped");