using CurrencyRates.Microservices.Rates.Domain.Aggregates;

namespace CurrencyRates.Microservices.Rates.Application.Interfaces;

public interface ISourceSyncJobManager
{
    Task RegisterAsync(Source source, string defaultCronExpression, bool archiveSynchronized);
}
