namespace MarketHub.Clients.Rates.Client.Options;

public class RatesApiOptions
{
    public const string RatesApi = nameof(RatesApi);
    public string BaseUri { get; set; } = default!;
}