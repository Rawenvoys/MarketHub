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

    private List<CurrencyRateDto>? currencyRates;

    public string TableNumber { get; set; } = string.Empty;


    public bool DisplayCurrencyRates => currencyRates != null && currencyRates.Any();

    private Grid<CurrencyRateDto> currencyRatesGrid = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            currencyRates = new List<CurrencyRateDto>();
            var currencyRateTable = await RatesApi.GetLastTableAsync(default);
            if (currencyRateTable != null)
            {
                Logger.LogInformation("Successfully fetched currency rates for table {TableNumber}.", currencyRateTable.Number);
                TableNumber = $"{currencyRateTable.Number} - {currencyRateTable.EffectiveDate}";
                if (currencyRateTable.Rates == null)
                    return;

                foreach (var currencyRate in currencyRateTable.Rates)
                {
                    Logger.LogInformation("Add `{CurrencyCode} - {CurrencyName}", currencyRate.Code, currencyRate.Name);
                    currencyRates!.Add(currencyRate);
                }

                await currencyRatesGrid.RefreshDataAsync();
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching currency rates.");
            // Optionally, handle the error more gracefully, e.g., display a message to the user. For now, we'll just log it and proceed with an empty list.
        }
    }

}
