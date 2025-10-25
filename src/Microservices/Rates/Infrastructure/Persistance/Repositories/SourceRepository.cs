using MarketHub.Microservices.Rates.Domain.Aggregates;
using MarketHub.Microservices.Rates.Domain.Enums.Source;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Repositories;

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
