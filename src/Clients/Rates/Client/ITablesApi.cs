using MarketHub.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;

namespace MarketHub.Clients.Rates.Client;

public interface ITablesApi
{
    [Get("")]
    Task<CurrencyRateTableDto> GetLastTableAsync(CancellationToken cancellationToken);
}
