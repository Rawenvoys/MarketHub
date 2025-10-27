using MarketHub.Microservices.Rates.Domain.Enums.Source;

namespace MarketHub.Microservices.Rates.Domain.Interfaces.States;

public interface ISyncState
{
    public Guid SourceId { get; set; }
    public bool ArchiveSynchronized { get; set; }
}
