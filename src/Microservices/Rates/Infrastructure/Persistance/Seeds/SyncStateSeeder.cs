using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.States;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.States;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Seeds;

public class SyncStateSeeder(ISyncStateRepository syncStateRepository, ILogger<SyncStateSeeder> logger)
{
    private readonly ISyncStateRepository _syncStateRepository = syncStateRepository;
    private readonly ILogger<SyncStateSeeder> _logger = logger;

    //ToDo: Implement Merge or Upsert - reduce call to cosmos container to 1 per seed
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        foreach(var syncStateSeed in SyncStateSeeds)
        {
            _logger.LogInformation("Start seeding sync state for SourceId: {SourceId}", syncStateSeed.SourceId);

            var syncState = await _syncStateRepository.GetAsync(syncStateSeed.SourceId, cancellationToken);
            if (syncState != null)
                return;

            _logger.LogInformation("Add missing sync state document for SourceId: {SourceId}", syncStateSeed.SourceId);
            await _syncStateRepository.SaveAsync(syncStateSeed, cancellationToken);
        }
    }

    private static ISyncState NbpApiDateRangeSyncState
        => new NbpApiDateRangeSyncState()
        {
            SourceId = Guid.Parse("d07ebbb0-ee4b-4d13-8ef7-8ef007ae77e3"),
            NextSyncFrom = DateOnly.Parse("2002-01-02"),
            NextSyncTo = DateOnly.FromDateTime(DateTime.UtcNow),
            ArchiveSynchronized = false
        };

    private static List<ISyncState> SyncStateSeeds
        => [NbpApiDateRangeSyncState];
}
