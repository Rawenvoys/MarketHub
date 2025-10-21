namespace CurrencyRates.Microservices.Rates.Domain.Entities;

public class CurrencyRate
{

    public Guid Id { get; private set; }
    public Guid TableId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public Currency Currency { get; private set; }
    public Rate Rate { get; private set; }
}
