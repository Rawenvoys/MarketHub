using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;

public interface ITableRepository
{
    Task AddAsync(Table table, CancellationToken cancellationToken);

    Task<Table> GetLatestAsync(CancellationToken cancellationToken = default);
}
