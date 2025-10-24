using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Factories;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class ArchiveSourceSyncService(ISyncStrategyFactory strategyFactory, ILogger<ArchiveSourceSyncService> logger) : IArchiveSourceSyncService
{
    private readonly ILogger<ArchiveSourceSyncService> _logger = logger;
    private readonly ISyncStrategyFactory _syncStrategyFactory = strategyFactory;
    public async Task ExecuteAsync(Source source, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting archive source sync for SourceId: {SourceId}", source.Id);
        var strategy = _syncStrategyFactory.GetStrategy(source.SyncStrategy);
        await strategy.ExecuteAsync(source.Id, cancellationToken);
        // throw new NotImplementedException();
    }
}
