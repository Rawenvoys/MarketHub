using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Application.Interfaces;

public interface ISourceSyncService
{
    Task ExecuteAsync(Guid id, string syncStrategy, CancellationToken cancellationToken = default);
}
