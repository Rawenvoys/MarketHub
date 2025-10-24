using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Currency;

namespace CurrencyRates.Microservices.Rates.Domain.Entities;

public class Currency
{
    public Guid Id { get; private set; }
    public Code Code { get; private set; }
    public Name Name { get; private set; }    
    public virtual ICollection<CurrencyRate> CurrencyRates { get; private set; } = [];
    

    private Currency() { }

    private Currency(Guid id, Code code, Name name)
    {
        Id = id;
        Code = code;
        Name = name;
    }

    public static Currency Create(Guid id, Code code, Name name)
        => new(id, code, name);
}
