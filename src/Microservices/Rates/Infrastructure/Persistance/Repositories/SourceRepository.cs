using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.Interfaces;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class SourceRepository(RatesDbContext ratesDbContext) : ISourceRepository
{
    private readonly RatesDbContext _ratesDbContext = ratesDbContext;
    public async Task<IEnumerable<Source>> GetActiveAsync(CancellationToken cancellationToken)
        => await _ratesDbContext.Sources.Where(s => s.Status.Value.Equals(Status.Active.Value)).ToListAsync(cancellationToken);

    public async Task SaveAsync(Source source)
    {
        _ratesDbContext.Sources.Update(source);
        await _ratesDbContext.SaveChangesAsync();
    }
}
