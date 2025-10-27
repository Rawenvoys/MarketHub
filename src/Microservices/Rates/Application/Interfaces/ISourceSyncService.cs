using MarketHub.Microservices.Rates.Domain.Aggregates;

namespace MarketHub.Microservices.Rates.Application.Interfaces;

public interface ISourceSyncService
{
    Task ExecuteAsync(Guid id, string syncStrategy, CancellationToken cancellationToken = default);
}
