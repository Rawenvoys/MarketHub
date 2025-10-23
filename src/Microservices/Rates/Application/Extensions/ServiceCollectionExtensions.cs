using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CurrencyRates.Microservices.Rates.Infrastructure.Extensions;
using Hangfire;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;

namespace CurrencyRates.Microservices.Rates.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    private static void AddHangfireWithServer(this IServiceCollection services, IConfiguration configuration)
    {
               var connectionString = configuration.GetConnectionString(RatesDbContext.ConnectionStringName)
            ?? throw new InvalidOperationException($"Cannot find connection string '{RatesDbContext.ConnectionStringName}'");
        services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    // .UseSimpleBackgroundJobFactory() ???
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new Hangfire.SqlServer.SqlServerStorageOptions
    {
        // Upewnij się, że tabela jobów będzie miała prefiks np. "Hangfire"
        SchemaName = "Hangfire", 
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true // Zabezpieczenie przed deadlocks
    }));

// Rejestracja Serwera Hangfire (dla przetwarzania jobów)
services.AddHangfireServer(options => 
{
    // Konfiguracja wątków
    options.WorkerCount = Environment.ProcessorCount * 2; 
    options.Queues = ["default", "critical"]; // Można definiować kolejki
});
    }
}
