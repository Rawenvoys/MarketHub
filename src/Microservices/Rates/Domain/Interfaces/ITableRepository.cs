using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces;

public interface ITableRepository
{
    Task SaveAsync(Table table);
}
