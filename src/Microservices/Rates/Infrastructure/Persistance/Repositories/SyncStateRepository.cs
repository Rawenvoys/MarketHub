using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.States;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.States;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class SyncStateRepository(Func<string, Container> containerFactory, ILogger<SyncStateRepository> logger) : ISyncStateRepository
{
    private readonly Container _container = containerFactory(SyncStatesContainerName);
    private readonly ILogger<SyncStateRepository> _logger = logger;
    private const string SyncStatesContainerName = "SyncStates";

    public async Task<ISyncState> GetAsync(Guid sourceId, CancellationToken cancellationToken)
        => (await _container.ReadItemAsync<NbpApiDateRangeSyncState>(sourceId.ToString(), new PartitionKey(sourceId.ToString()), cancellationToken: cancellationToken)).Resource;

    public async Task SaveAsync(ISyncState syncState, CancellationToken cancellationToken)
    {
        if (syncState is not NbpApiDateRangeSyncState)
            throw new ArgumentException("Provided ISyncState is not a supported NbpApiDateRangeSyncState for this container.");
        var partitionKey = new PartitionKey(syncState.SourceId.ToString());
        var response = await _container.UpsertItemAsync(syncState, partitionKey, cancellationToken: cancellationToken);
        _logger.LogDebug("[ActivityId: {ActivityId}] - Sync state for {SourceId} saved successfully. Status: {StatusCode}", response.ActivityId, syncState.SourceId, response.StatusCode);
    }
}
