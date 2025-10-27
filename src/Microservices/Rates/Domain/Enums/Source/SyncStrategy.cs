using Ardalis.SmartEnum;

namespace MarketHub.Microservices.Rates.Domain.Enums.Source;

public sealed class SyncStrategy : SmartEnum<SyncStrategy, string>
{
    private SyncStrategy(string value) : base(value, value) { }


    public static readonly SyncStrategy NbpApiDateRange = new(nameof(NbpApiDateRange));
    public static readonly SyncStrategy NotImplemented = new(nameof(NotImplemented));
}
