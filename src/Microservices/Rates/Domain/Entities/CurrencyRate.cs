namespace CurrencyRates.Microservices.Rates.Domain.Entities;

public class CurrencyRate
{

    public Guid Id { get; private set; }
    public Guid TableId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public Currency Currency { get; private set; } = default!;
    public Rate Rate { get; private set; }

    private CurrencyRate() { }

    private CurrencyRate(Guid id, Guid tableId, Guid currencyId, Rate rate)
    {
        Id = id;
        TableId = tableId;
        CurrencyId = currencyId;
        Rate = rate;
    }

    public static CurrencyRate Create(Guid id, Guid tableId, Guid currencyId, Rate rate)
        => new(id, tableId, currencyId, rate);
        
}
