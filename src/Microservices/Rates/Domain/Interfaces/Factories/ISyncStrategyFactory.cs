
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Strategies;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.Factories;

public interface ISyncStrategyFactory
{
    ISyncStrategy GetStrategy(SyncStrategy syncStrategy);
}
