using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Table;
using Type = CurrencyRates.Microservices.Rates.Domain.Enums.Table.Type;

namespace CurrencyRates.Microservices.Rates.Domain.Aggregates;

public class Table : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Type Type { get; private set; }
    public Number Number { get; private set; }
    public DateOnly EffectiveDate { get; private set; }

    private readonly List<CurrencyRate> _rates = [];
    public IReadOnlyCollection<CurrencyRate> Rates => _rates;
}
