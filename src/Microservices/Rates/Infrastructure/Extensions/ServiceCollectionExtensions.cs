using CurrencyRates.Clients.Nbp.Client.Extensions;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Factories;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Strategies;
using CurrencyRates.Microservices.Rates.Infrastructure.Factories;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Extensions;
using CurrencyRates.Microservices.Rates.Infrastructure.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistance(configuration);
        services.AddNbpApiClient(configuration);
        services.AddStrategies();
        services.AddFactories();
    }

    private static void AddStrategies(this IServiceCollection services)
        => services.AddTransient<ISyncStrategy, NbpApiDateRangeSyncStrategy>();

    private static void AddFactories(this IServiceCollection services)
        => services.AddTransient<ISyncStrategyFactory, SyncStrategyFactory>();
}