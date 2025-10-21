using CurrencyRates.Microservices.Rates.Domain.ValueObjects;

namespace CurrencyRates.Microservices.Rates.Domain.Entities;

public class Currency : Entity<Guid>
{
    public Currency(Guid id) : base(id)
    {
    }

    public Code Code { get; set; }
    public Name Name { get; set; }
}
