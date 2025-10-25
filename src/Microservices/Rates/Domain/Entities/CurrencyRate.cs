using MarketHub.Microservices.Rates.Domain.Aggregates;

namespace MarketHub.Microservices.Rates.Domain.Entities;

public class CurrencyRate
{

    public Guid Id { get; private set; }
    public Rate Mid { get; private set; }

    public Guid TableId { get; set; }
    public Table Table { get; set; } = null!;

    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;

    private CurrencyRate() { }

    private CurrencyRate(Guid id, Guid tableId, Guid currencyId, Rate mid)
    {
        Id = Guid.Empty == id ? Guid.NewGuid() : id;
        TableId = tableId;
        CurrencyId = currencyId;
        Mid = mid;
    }

    public static CurrencyRate Create(Guid tableId, Guid currencyId, Rate mid)
        => new(Guid.Empty, tableId, currencyId, mid);

    public void SetCurrency(Currency currency)
    {
        Currency = currency;
    }

    public void SetTable(Table table)
    {
        Table = table;
    }
}
