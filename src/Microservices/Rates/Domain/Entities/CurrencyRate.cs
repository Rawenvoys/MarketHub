using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Domain.Entities;

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

    public static CurrencyRate Create(Guid id, Guid tableId, Guid currencyId, Rate mid)
        => new(id, tableId, currencyId, mid);
}
