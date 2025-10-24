using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Table;
using Type = CurrencyRates.Microservices.Rates.Domain.Enums.Table.Type;

namespace CurrencyRates.Microservices.Rates.Domain.Aggregates;

public class Table : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Type Type { get; private set; }
    public Number Number { get; private set; }
    public DateOnly EffectiveDate { get; private set; }

    private readonly List<CurrencyRate> _currencies = [];
    public IReadOnlyCollection<CurrencyRate> Currencies => _currencies;

    private Table() { }

    private Table(Type type, Number number, DateOnly effectiveDate)
    {
        Id = Guid.NewGuid();
        Type = type;
        Number = number;
        EffectiveDate = effectiveDate;
    }

    public static Table Create(Type type, Number number, DateOnly effectiveDate)
        => new(type, number, effectiveDate);
}
