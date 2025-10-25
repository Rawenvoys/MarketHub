namespace MarketHub.Clients.Nbp.Contracts.Dtos.ExchangeRates.Tables;

public record ExchangeRateTableDto([property: JsonPropertyName("table")] string Table,
                                   [property: JsonPropertyName("no")] string No,
                                   [property: JsonPropertyName("effectiveDate")] DateOnly EffectiveDate,
                                   [property: JsonPropertyName("rates")] IReadOnlyList<ExchangeRateDto> Rates);