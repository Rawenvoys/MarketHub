using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;

public interface ISourceRepository
{
    Task<IEnumerable<Source>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(Source source, CancellationToken cancellationToken = default);
}
