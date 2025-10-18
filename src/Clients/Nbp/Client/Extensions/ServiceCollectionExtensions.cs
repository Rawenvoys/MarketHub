

namespace CurrencyRates.Clients.Nbp.Client.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly Action<IServiceProvider, HttpClient> _configureNbpApiClientAction = (serviceProvider, httpClient) =>
        {
            var nbpApiOptions = serviceProvider.GetRequiredService<IOptions<NbpApiOptions>>().Value
                ?? throw new InvalidOperationException($"Failed to load required NBP API configuration. Ensure the {NbpApiOptions.NbpApiClient} section is correctly defined in appsettings.json and available in the DI container.");

            if (string.IsNullOrWhiteSpace(nbpApiOptions.BaseUri))
                throw new InvalidOperationException("NBP API BaseUri is missing in configuration.");

            httpClient.BaseAddress = new Uri(nbpApiOptions.BaseUri);
        };

    private static void AddNbpApiOptions(this IServiceCollection services, IConfiguration configuration) 
        => services.AddOptions<NbpApiOptions>()
                   .Bind(configuration.GetSection(NbpApiOptions.NbpApiClient));

    public static void AddNbpApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNbpApiOptions(configuration);
        services.AddRefitClient<INbpApi>().ConfigureHttpClient(_configureNbpApiClientAction);
    }
}
