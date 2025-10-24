using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CurrencyRates.Microservices.Rates.Infrastructure.Extensions;
using Hangfire;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using CurrencyRates.Microservices.Rates.Application.Services;
using CurrencyRates.Microservices.Rates.Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace CurrencyRates.Microservices.Rates.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddHangfireWithServer(configuration);
        services.AddRecurringJobManager();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void AddRecurringJobManager(this IServiceCollection services)
    {
        services.AddSingleton<IRecurringJobManager, RecurringJobManager>();
        services.AddTransient<ISourceSyncJobManager, SourceSyncJobManager>();
        services.AddTransient<IHostedService, JobManager>();
    }

    public static void AddHangfireWithServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(RatesDbContext.ConnectionStringName)
            ?? throw new InvalidOperationException($"Cannot find connection string '{RatesDbContext.ConnectionStringName}'");

        services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new Hangfire.SqlServer.SqlServerStorageOptions
                {
                    SchemaName = "hangfire",
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Environment.ProcessorCount * 2;
            options.Queues = ["default", "critical"];
        });
    }
}
