using System;
using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces;

public interface ISourceRepository
{
    Task<IEnumerable<Source>> GetActiveAsync(CancellationToken cancellationToken);
    Task SaveAsync(Source source);
}
