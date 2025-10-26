using MarketHub.Microservices.Rates.Domain.Interfaces.States;
using Newtonsoft.Json;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.States;

public class NbpApiDateRangeSyncState : ISyncState
{
    [JsonProperty(PropertyName = "id")]
    public Guid SourceId { get; set; }

    [JsonProperty(PropertyName = "archiveSynchronized")]
    public bool ArchiveSynchronized { get; set; }

    [JsonProperty(PropertyName = "nextSyncAt")]
    public DateOnly NextSyncAt { get; set; }

    [JsonProperty(PropertyName = "createdAt")]
    public DateOnly CreatedAt { get; set; }


}
