using Ardalis.SmartEnum;

namespace CurrencyRates.Microservices.Rates.Domain.Enums.Source;

public sealed class SyncStrategy : SmartEnum<SyncStrategy, string>
{
    private SyncStrategy(string value) : base(value, value) { }

    public readonly SyncStrategy NbpApiDateRange = new(nameof(NbpApiDateRange));
}
