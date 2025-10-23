namespace CurrencyRates.Microservices.Rates.Application.Interfaces;
public interface ISourceSyncJobManager
{
    Task RegisterAsync(Guid sourceId, string syncStrategyName, string sourceCronExpression, string defaultCronExpression, bool archiveSynchronized);
}
