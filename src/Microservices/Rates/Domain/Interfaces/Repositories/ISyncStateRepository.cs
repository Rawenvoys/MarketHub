using MarketHub.Microservices.Rates.Domain.Interfaces.States;

namespace MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;

public interface ISyncStateRepository
{
    Task<ISyncState> GetAsync(Guid sourceId, CancellationToken cancellationToken);
    Task SaveAsync(ISyncState syncState, CancellationToken cancellationToken);
}
