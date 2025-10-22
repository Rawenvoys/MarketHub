using CurrencyRates.Microservices.Rates.Domain.Enums.Source;

namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.States;

public interface ISyncState
{
    public Guid SourceId { get; set; }
}
