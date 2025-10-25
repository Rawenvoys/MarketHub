using MarketHub.Microservices.Rates.Domain.Aggregates;

namespace MarketHub.Microservices.Rates.Application.Interfaces;

public interface ISourceSyncJobManager
{
    Task RegisterAsync(Source source, string defaultCronExpression, bool archiveSynchronized);
}
