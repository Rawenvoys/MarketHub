namespace MarketHub.Clients.Rates.Contracts.Dtos.LatestCurrencyRates;

public record CurrencyRateTableDto(string Number, DateOnly EffectiveDate, IList<CurrencyRateDto> Rates);
