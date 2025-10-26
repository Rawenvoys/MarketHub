using MarketHub.Clients.Nbp.Client.Extensions;
using MarketHub.Microservices.Rates.Domain.Interfaces.Factories;
using MarketHub.Microservices.Rates.Domain.Interfaces.Strategies;
using MarketHub.Microservices.Rates.Infrastructure.Factories;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Extensions;
using MarketHub.Microservices.Rates.Infrastructure.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarketHub.Microservices.Rates.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistance(configuration);
        services.AddRatesCosmos(configuration);
        services.AddNbpApiClient(configuration);
        services.AddStrategies();
        services.AddFactories();
    }

    private static void AddStrategies(this IServiceCollection services)
        => services.AddScoped<NbpApiDateRangeSyncStrategy>();

    private static void AddFactories(this IServiceCollection services)
        => services.AddScoped<ISyncStrategyFactory, SyncStrategyFactory>();
}