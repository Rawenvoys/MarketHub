using CurrencyRates.Microservices.Rates.Domain.Enums.Source;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.SyncState;

public interface ISyncState
{
    public Guid SourceId { get; set; }
    public SyncStrategy StrategyType { get; set; }
}
