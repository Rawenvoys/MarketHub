namespace CurrencyRates.Clients.Nbp.Client;

public interface IExchangeRatesTablesApi
{
    [Get("/exchangerates/tables/{table}/{startDate}/{endDate}/")]
    Task<IReadOnlyList<ExchangeRateTableDto>> Get([AliasAs("table")] string type, [AliasAs("startDate")]  DateOnly from, [AliasAs("endDate")]  DateOnly to);
}
