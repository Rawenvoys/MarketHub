namespace MarketHub.Clients.Nbp.Client.Options;

public class NbpApiOptions
{
    public const string NbpApi = nameof(NbpApi);
    public string BaseUri { get; set; } = default!;
}
