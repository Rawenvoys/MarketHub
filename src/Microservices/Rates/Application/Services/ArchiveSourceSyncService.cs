using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Factories;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Application.Services;

public class ArchiveSourceSyncService(ISyncStrategyFactory strategyFactory, ILogger<ArchiveSourceSyncService> logger) : IArchiveSourceSyncService
{
    private readonly ILogger<ArchiveSourceSyncService> _logger = logger;
    private readonly ISyncStrategyFactory _syncStrategyFactory = strategyFactory;
    [DisableConcurrentExecution(10 * 60)]
    public async Task ExecuteAsync(Guid id, string syncStrategy, CancellationToken cancellationToken = default)
    {
        var strategy = _syncStrategyFactory.GetStrategy(SyncStrategy.FromValue(syncStrategy));
        await strategy.ExecuteAsync(id, cancellationToken);
    }
}
