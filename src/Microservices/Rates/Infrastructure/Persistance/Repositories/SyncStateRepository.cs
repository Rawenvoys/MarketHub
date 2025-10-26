using System.Net;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using MarketHub.Microservices.Rates.Domain.Interfaces.States;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.States;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class SyncStateRepository(Func<string, Container> containerFactory, ILogger<SyncStateRepository> logger) : ISyncStateRepository
{
    private readonly Container _container = containerFactory(SyncStatesContainerName);
    private readonly ILogger<SyncStateRepository> _logger = logger;
    private const string SyncStatesContainerName = "SyncStates";

    //ToDo: Implement ContainerReadItemWrapper
    public async Task<ISyncState> GetAsync(Guid sourceId, CancellationToken cancellationToken)
    {
        try
        {
            return (await _container.ReadItemAsync<NbpApiDateRangeSyncState>(sourceId.ToString(), new PartitionKey(sourceId.ToString()), cancellationToken: cancellationToken)).Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Sync state document not found in CosmosDB for SourceId: {SourceId}", sourceId);
            return default!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading sync state for {SourceId}", sourceId);
            throw;
        }
    }

    public async Task SaveAsync(ISyncState syncState, CancellationToken cancellationToken)
    {
        if (syncState is not NbpApiDateRangeSyncState nbpApiDateRangeSyncState)
            throw new ArgumentException("Provided ISyncState is not a supported NbpApiDateRangeSyncState for this container");
        var partitionKey = new PartitionKey(syncState.SourceId.ToString());
        var response = await _container.UpsertItemAsync(nbpApiDateRangeSyncState, partitionKey, cancellationToken: cancellationToken);
        _logger.LogInformation("[ActivityId: {ActivityId}] - Sync state for {SourceId} saved successfully. Status: {StatusCode}", response.ActivityId, nbpApiDateRangeSyncState.SourceId, response.StatusCode);
    }
}
