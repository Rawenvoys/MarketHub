using MarketHub.Microservices.Rates.Domain.ValueObjects.Table;
using Type = MarketHub.Microservices.Rates.Domain.Enums.Table.Type;

namespace MarketHub.Microservices.Rates.Domain.Aggregates;

public class Table : IAggregateRoot
{
    public Guid Id { get; private set; }
    public Type Type { get; private set; }
    public Number Number { get; private set; }
    public DateOnly EffectiveDate { get; private set; }

    public Guid SourceId { get; set; }
    public Source Source { get; set; } = null!;

    public ICollection<CurrencyRate> CurrencyRates { get; private set; } = [];

    private Table() { }

    private Table(Guid id, Type type, Guid sourceId, Number number, DateOnly effectiveDate)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Type = type;
        Number = number;
        EffectiveDate = effectiveDate;
        SourceId = sourceId;
    }

    public static Table Create(Type type, Number number, DateOnly effectiveDate, Guid sourceId)
        => new(Guid.Empty, type, sourceId, number, effectiveDate);

    public void AddCurrencyRates(IEnumerable<CurrencyRate> currencyRates)
    {
        foreach (var currencyRate in currencyRates)
            CurrencyRates.Add(currencyRate);
    }

    public void SetSource(Source source)
    {
        Source = source;
    }

}
