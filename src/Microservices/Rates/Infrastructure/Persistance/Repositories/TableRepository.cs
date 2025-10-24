using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class TableRepository(RatesDbContext ratesDbContext) : ITableRepository
{
    private readonly RatesDbContext _ratesDbContext = ratesDbContext;
    public Task SaveAsync(Table table, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(table);
        _ratesDbContext.Tables.Update(table);
        return _ratesDbContext.SaveChangesAsync(cancellationToken);

    }
}
