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


    protected override async Task OnInitializedAsync()
    {
    }
}
