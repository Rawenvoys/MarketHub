using MarketHub.Clients.Rates.Client.Options;
using Refit;

namespace MarketHub.Clients.Rates.Client.Extensions;

public static class ServiceCollectionExtensions
{
    private static void AddRatesApiOptions(this IServiceCollection services, IConfiguration configuration)
        => services.AddOptions<RatesApiOptions>()
                   .Bind(configuration.GetSection(RatesApiOptions.RatesApi));

    public static void AddRatesApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRatesApiOptions(configuration);

        var ratesApiOptions = configuration.GetSection(RatesApiOptions.RatesApi).Get<RatesApiOptions>();

        if (ratesApiOptions == null || string.IsNullOrWhiteSpace(ratesApiOptions.BaseUri))
        {
            throw new InvalidOperationException($"Failed to load required Rates API configuration. Ensure the {RatesApiOptions.RatesApi} section is correctly defined in appsettings.json.");
        }

        services.AddHttpClient("RatesApi", client =>
        {
            client.BaseAddress = new Uri(ratesApiOptions.BaseUri);
        })
        .AddTypedClient(httpClient => RestService.For<IRatesApi>(httpClient));
    }
}
