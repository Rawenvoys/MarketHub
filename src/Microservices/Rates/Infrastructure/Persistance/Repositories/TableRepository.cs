using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Entities;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Repositories;

public class TableRepository(RatesDbContext ratesDbContext) : ITableRepository
{
    private readonly RatesDbContext _ratesDbContext = ratesDbContext;

    public async Task AddAsync(Table table, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(table);

        await using var transaction = await _ratesDbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
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
                    var dbCurrency = (await _ratesDbContext.Currencies
                        .ToListAsync(cancellationToken)).FirstOrDefault(c => c.Code.Value == currencyCode);

                    if (dbCurrency != null)
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
}
