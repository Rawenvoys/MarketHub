using MarketHub.Microservices.Rates.Domain.Aggregates;

namespace MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;

public interface ISourceRepository
{
    Task<Source?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IList<Source>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(Source source, CancellationToken cancellationToken = default);
}
