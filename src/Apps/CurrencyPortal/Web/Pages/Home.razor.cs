using BlazorBootstrap;
using MarketHub.Clients.Rates.Client;
using MarketHub.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MarketHub.Apps.CurrencyPortal.Web.Pages;

public partial class Home
{
    [Inject]
    public IRatesApi RatesApi { get; set; } = default!;

    [Inject]
    public ILogger<Home> Logger { get; set; } = default!;

    [Inject]
    protected PreloadService PreloadService { get; set; } = default!;

    private List<CurrencyRateDto> currencyRates = [];



    public bool DisplayCurrencyRates => currencyRates.Count != 0;



    protected override async Task OnInitializedAsync()
    {
        Logger.LogWarning("[{DateTime}] [Home] - Start `OnInitializedAsync`", DateTime.UtcNow);
        var currencyRateTable = await RatesApi.GetLastTableAsync(default);
        // currencyRates = currencyRateTable?.Rates.ToList() ?? [];
        Logger.LogWarning("[{DateTime}] [Home] - Finished `OnInitializedAsync` with data: {CurrencyRates}", DateTime.UtcNow, JsonConvert.SerializeObject(currencyRateTable));
    }

}
