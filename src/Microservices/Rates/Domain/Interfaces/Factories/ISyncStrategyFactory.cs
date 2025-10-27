
using MarketHub.Microservices.Rates.Domain.Enums.Source;
using MarketHub.Microservices.Rates.Domain.Interfaces.Strategies;

namespace MarketHub.Microservices.Rates.Domain.Interfaces.Factories;

public interface ISyncStrategyFactory
{
    ISyncStrategy GetStrategy(SyncStrategy syncStrategy);
}
