using System.Collections.Generic;
using MarketHub.Clients.Nbp.Client;
using MarketHub.Clients.Rates.Client;
using MarketHub.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;
using Microsoft.AspNetCore.Components;
namespace MarketHub.Apps.CurrencyPortal.Web.Pages;

public partial class Home
{
    [Inject]
    public INbpApi RatesApi { get; set; } = default!;
    private IEnumerable<CurrencyRateDto> currencyRates = [];

    private async Task<GridDataProviderResult<CurrencyRateDto>> CurrencyRatesDataProvider(GridDataProviderRequest<CurrencyRateDto> request, CancellationToken cancellationToken)
    {
        // var table = await RatesApi.GetLastTableAsync(cancellationToken);
        // currencyRates ??= table.Rates;
        return await Task.FromResult(request.ApplyTo(currencyRates));
    }
}
