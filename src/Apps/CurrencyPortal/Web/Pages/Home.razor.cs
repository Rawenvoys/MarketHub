using BlazorBootstrap;
using MarketHub.Clients.Rates.Client;
using MarketHub.Clients.Rates.Contracts.Dtos.GetLastTable;
using Microsoft.AspNetCore.Components;
using BlazorBootstrap.Components;

namespace MarketHub.Apps.CurrencyPortal.Web.Pages;

public partial class Home
{
    [Inject]
    public IRatesApi RatesApi { get; set; } = default!;

    [Inject]
    public ILogger<Home> Logger { get; set; } = default!;

    private List<CurrencyRateDto> currencyRates = [];

    private string tableTitle = default!;

    private Grid<CurrencyRateDto> currencyRatesGrid = default!;

    private Guid selectedSourceId = default!;
    private int selectedYear = default!;
    private int selectedMonth = default!;

    private IList<SelectOption<Guid>> sourceOptions = [];
    private IList<SelectOption<int>> yearOptions = [];
    private readonly IList<SelectOption<int>> monthOptions = [
        new SelectOption<int>(1, "January"),
        new SelectOption<int>(2, "February"),
        new SelectOption<int>(3, "March"),
        new SelectOption<int>(4, "April"),
        new SelectOption<int>(5, "May"),
        new SelectOption<int>(6, "June"),
        new SelectOption<int>(7, "July"),
        new SelectOption<int>(8, "August"),
        new SelectOption<int>(9, "September"),
        new SelectOption<int>(10, "October"),
        new SelectOption<int>(11, "November"),
        new SelectOption<int>(12, "December")
    ];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var meta = await RatesApi.GetMetaAsync(default);
            var currentMeta = meta.Sources.OrderByDescending(s => s.Timeframe.EndYear).ThenBy(s => s.Timeframe.EndMonth).FirstOrDefault();
            if (currentMeta is null) //No configuration. Inform user.
                return;

            selectedSourceId = currentMeta.Id;
            selectedYear = currentMeta.Timeframe.EndYear;
            selectedMonth = currentMeta.Timeframe.EndMonth;

            //Get tables in month
            var currencyRateTable = await RatesApi.GetLastTableAsync(default);
            if (currencyRateTable is null)
                return;

            tableTitle = $"Tabela zr {currencyRateTable.Number} z dnia {currencyRateTable.EffectiveDate:yyyy-MM-dd}";
            if (currencyRateTable.Rates is null || !currencyRateTable.Rates.Any())
                return;

            currencyRates.Clear();
            currencyRates!.AddRange(currencyRateTable.Rates);
            await currencyRatesGrid.RefreshDataAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching currency rates.");
        }
    }
}


public record SelectOption<T>(T Id, string Text);