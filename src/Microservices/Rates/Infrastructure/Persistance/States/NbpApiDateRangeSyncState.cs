using CurrencyRates.Microservices.Rates.Domain.Interfaces.States;
using Newtonsoft.Json;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.States;

public class NbpApiDateRangeSyncState : ISyncState
{
    [JsonProperty(PropertyName = "id")]
    public Guid SourceId { get; set; }

    [JsonProperty(PropertyName = "archiveSynchronized")]
    public bool ArchiveSynchronized { get; set; }
    
    [JsonProperty(PropertyName = "nextSyncFrom")]
    public DateOnly NextSyncFrom { get; set; }

    [JsonProperty(PropertyName = "nextSyncTo")]
    public DateOnly NextSyncTo { get; set; }
    

}
