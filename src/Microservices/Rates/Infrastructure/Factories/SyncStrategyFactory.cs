using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Factories;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Strategies;
using CurrencyRates.Microservices.Rates.Infrastructure.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Factories;

public class SyncStrategyFactory(IServiceProvider serviceProvider) : ISyncStrategyFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public ISyncStrategy GetStrategy(SyncStrategy syncStrategy)
    {
        return syncStrategy.Value switch
        {
            var strategy when strategy.Equals(SyncStrategy.NbpApiDateRange.Value) => _serviceProvider.GetRequiredService<NbpApiDateRangeSyncStrategy>(),
            _ => throw new ArgumentException($"Strategy '{syncStrategy.Value}' not supported or not registered"),
        };
    }
}
