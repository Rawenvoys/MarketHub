namespace CurrencyRates.Clients.Nbp.Contracts.Dtos.ExchangeRates.Tables;
public record ExchangeRateDto([property: JsonPropertyName("currency")] string Currency,
                              [property: JsonPropertyName("code")] string Code,
                              [property: JsonPropertyName("mid")] decimal Mid);