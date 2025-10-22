using CurrencyRates.Microservices.Rates.Domain.Interfaces.States;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;

public interface ISyncStateRepository
{
    Task<ISyncState> GetAsync(CancellationToken cancellationToken);
    Task SaveAsync(ISyncState syncState, CancellationToken cancellationToken);
}
