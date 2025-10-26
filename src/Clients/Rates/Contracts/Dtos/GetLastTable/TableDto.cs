namespace MarketHub.Clients.Rates.Contracts.Dtos.GetLastTable;

public record TableDto(string Number, DateOnly EffectiveDate, IList<CurrencyRateDto> Rates);