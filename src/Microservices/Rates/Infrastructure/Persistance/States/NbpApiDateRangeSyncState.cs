using CurrencyRates.Microservices.Rates.Domain.Interfaces.States;
using Newtonsoft.Json;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.States;

public class NbpApiDateRangeSyncState : ISyncState
{
    [JsonProperty(PropertyName = "sourceId")]
    public Guid SourceId { get; set; }

    [JsonProperty(PropertyName = "nextSyncDate")]
    public DateOnly NextSyncDate { get; set; }
}
