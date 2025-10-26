using BlazorBootstrap;
using MarketHub.Clients.Nbp.Client;
using MarketHub.Clients.Nbp.Contracts.Dtos.ExchangeRates.Tables;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace MarketHub.Apps.CurrencyPortal.Web.Pages;

public partial class Home
{
    [Inject]
    public INbpApi NbpApi { get; set; } = default!;

    [Inject]
    public ILogger<Home> Logger { get; set; } = default!;

    public DateOnly FromDate { get; set; } = default!;
    public DateOnly ToDate { get; set; } = default!;


    public List<ExchangeRateTableDto> ExchangeRateTables { get; set; } = default!;

    public string? LoadingMessage { get; set; } = "Loading...";

    public bool DisplayLoading => !string.IsNullOrWhiteSpace(LoadingMessage);



    protected override async Task OnInitializedAsync()
    {
        FromDate = DateOnly.Parse("2002-01-02");
        ToDate = DateOnly.Parse("2002-03-31");
        // Logger.LogWarning("START: FromDate {FromDate} ToDate {ToDate}", FromDate.ToString(), ToDate.ToString());

        var result = await NbpApi.GetAsync("B", FromDate.ToShortDateString(), ToDate.ToShortDateString(), default);
        // Logger.LogWarning("ExchangeRateTableDataProvider API call {json}", json);
        ExchangeRateTables = [.. result];
        // Logger.LogWarning("END: FromDate {FromDate} ToDate {ToDate}", FromDate.ToString(), ToDate.ToString());
        LoadingMessage = null;
    }

}
