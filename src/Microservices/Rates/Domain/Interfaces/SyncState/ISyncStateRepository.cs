namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.SyncState;

public interface ISyncStateRepository
{
    Task<ISyncState> GetAsync(CancellationToken cancellationToken);
    Task SaveAsync(ISyncState syncState, CancellationToken cancellationToken);
}
