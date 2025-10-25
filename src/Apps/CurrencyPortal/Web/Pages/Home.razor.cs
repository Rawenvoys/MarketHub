using System.Collections.Generic;
using MarketHub.Clients.Rates.Client;
using MarketHub.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;
using Microsoft.AspNetCore.Components;
namespace MarketHub.Apps.CurrencyPortal.Web.Pages;

public partial class Home
{
    [Inject]
    protected IRatesApi RatesApi { get; set; } = default!;
    private IEnumerable<CurrencyRateDto> currencyRates = [];

    private async Task<GridDataProviderResult<CurrencyRateDto>> CurrencyRatesDataProvider(GridDataProviderRequest<CurrencyRateDto> request)
    {
        try
            {
        var table = await RatesApi.GetLastTableAsync(CancellationToken.None);
        currencyRates ??= table.Rates;
        return await Task.FromResult(request.ApplyTo(currencyRates));
            }
            catch (Exception ex)
        {

        }
    }

    protected override async Task OnInitializedAsync()
    {
    }
}
