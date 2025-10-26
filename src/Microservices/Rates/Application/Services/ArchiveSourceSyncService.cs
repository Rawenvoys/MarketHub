using MarketHub.Microservices.Rates.Application.Interfaces;
using MarketHub.Microservices.Rates.Domain.Enums.Source;
using MarketHub.Microservices.Rates.Domain.Interfaces.Factories;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace MarketHub.Microservices.Rates.Application.Services;

public class ArchiveSourceSyncService(ISyncStrategyFactory strategyFactory, ILogger<ArchiveSourceSyncService> logger) : IArchiveSourceSyncService
{
    private readonly ILogger<ArchiveSourceSyncService> _logger = logger;
    private readonly ISyncStrategyFactory _syncStrategyFactory = strategyFactory;
    [DisableConcurrentExecution(10 * 60)]
    public async Task ExecuteAsync(Guid id, string syncStrategy, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = DateTime.UtcNow;
        _logger.LogInformation("[{ExecutionDateTime}]: Resolve Archive Strategy for SourceId: {SourceId}", utcNow.ToString("yyyy-MM-dd hh:ss"), id);

        var strategy = _syncStrategyFactory.GetStrategy(SyncStrategy.FromValue(syncStrategy));
        await strategy.ExecuteAsync(id, cancellationToken);
    }
}
