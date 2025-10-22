namespace CurrencyRates.Microservices.Rates.Domain.Interfaces.Strategies;

public interface ISyncStrategy
{
    Task ExecuteAsync(Guid sourceId);
}