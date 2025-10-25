using MarketHub.Microservices.Rates.Domain.Aggregates;

namespace MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;

public interface ITableRepository
{
    Task AddAsync(Table table, CancellationToken cancellationToken);

    Task<Table> GetLatestAsync(CancellationToken cancellationToken = default);
}
