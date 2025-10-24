using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class SourceRepository(RatesDbContext ratesDbContext) : ISourceRepository
{
    private readonly RatesDbContext _ratesDbContext = ratesDbContext;
    public async Task<IList<Source>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var allSources = await _ratesDbContext.Sources.ToListAsync(cancellationToken);
        var activeSources = allSources.Where(s => s.Status == Status.Active).ToList();
        return activeSources;
    }

    public async Task<Source?> GetAsync(Guid id, CancellationToken cancellationToken = default) 
        => await _ratesDbContext.Sources.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task SaveAsync(Source source, CancellationToken cancellationToken = default)
    {
        _ratesDbContext.Sources.Update(source);
        await _ratesDbContext.SaveChangesAsync(cancellationToken);
    }
}
