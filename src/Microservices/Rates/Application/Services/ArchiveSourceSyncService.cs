using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Factories;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class ArchiveSourceSyncService(ISyncStrategyFactory strategyFactory) : IArchiveSourceSyncService
{
    private readonly ISyncStrategyFactory _syncStrategyFactory = strategyFactory;
    public Task ExecuteAsync(Source source, CancellationToken cancellationToken = default)
    {
        var strategy = _syncStrategyFactory.GetStrategy(source.SyncStrategy);
        strategy.ExecuteAsync(source.Id);
        throw new NotImplementedException();
    }
}
