using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Domain.Entities;

public class CurrencyRate
{

    public Guid Id { get; private set; }
    public Guid TableId { get; private set; }
    public virtual Table Table { get; private set; } = default!;
    public Guid CurrencyId { get; private set; }
    public virtual Currency Currency { get; private set; } = default!;
    public Rate Mid { get; private set; }

    private CurrencyRate() { }

    private CurrencyRate(Guid id, Guid tableId, Guid currencyId, Rate mid)
    {
        Id = Guid.Empty == id ? Guid.NewGuid() : id;
        TableId = tableId;
        CurrencyId = currencyId;
        Mid = mid;
    }

    public static CurrencyRate Create(Guid id, Guid tableId, Guid currencyId, Rate rate)
        => new(id, tableId, currencyId, rate);

    public void SetTable(Table table) => Table = table;

    public void SetCurrency(Currency currency) => Currency = currency;
}
