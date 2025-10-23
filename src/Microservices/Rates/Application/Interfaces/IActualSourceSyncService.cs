namespace CurrencyRates.Microservices.Rates.Application.Interfaces;
public interface IActualSourceSyncService : ISourceSyncService
{
    Task ExecuteAsync(Guid sourceId, CancellationToken cancellationToken = default);
}
