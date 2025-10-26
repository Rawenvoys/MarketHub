using MarketHub.Microservices.Rates.Domain.Aggregates;
using MarketHub.Microservices.Rates.Domain.Entities;
using MarketHub.Microservices.Rates.Domain.Enums.Source;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class TableRepository(RatesDbContext ratesDbContext) : ITableRepository
{
    private readonly RatesDbContext _ratesDbContext = ratesDbContext;

    public async Task AddAsync(Table table, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(table);

        var tableExists = await _ratesDbContext.Tables.AnyAsync(t => t.Number == table.Number, cancellationToken);
        if (tableExists)
        {
            return;
        }

        await using var transaction = await _ratesDbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var allCurrencies = await _ratesDbContext.Currencies.ToListAsync(cancellationToken);
            var currenciesByCode = allCurrencies.ToDictionary(c => c.Code.Value);
            var processedCurrencies = new Dictionary<string, Currency>();

            foreach (var currencyRate in table.CurrencyRates)
            {
                var currencyCode = currencyRate.Currency.Code.Value;
                if (processedCurrencies.TryGetValue(currencyCode, out var existingCurrency))
                {
                    currencyRate.Currency = existingCurrency;
                }
                else
                {
                    if (currenciesByCode.TryGetValue(currencyCode, out var dbCurrency))
                    {
                        currencyRate.Currency = dbCurrency;
                        processedCurrencies[currencyCode] = dbCurrency;
                    }
                    else
                    {
                        processedCurrencies[currencyCode] = currencyRate.Currency;
                    }
                }
            }

            _ratesDbContext.Tables.Add(table);

            await _ratesDbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Table> GetLatestAsync(CancellationToken cancellationToken = default)
        => await _ratesDbContext.Tables.Include(t => t.CurrencyRates)
                                       .ThenInclude(cr => cr.Currency)
                                       .OrderByDescending(t => t.EffectiveDate)
                                       .FirstOrDefaultAsync(cancellationToken) ?? throw new InvalidOperationException("No currency rates found.");
}
