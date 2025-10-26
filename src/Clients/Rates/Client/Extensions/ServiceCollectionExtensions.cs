using System.Net.Http.Headers;
using MarketHub.Clients.Rates.Client.Options;

namespace MarketHub.Clients.Rates.Client.Extensions;

public static class ServiceCollectionExtensions
{

    private static readonly Action<IServiceProvider, HttpClient> _configureRatesApiClientAction = (serviceProvider, httpClient) =>
        {
            var ratesApiOptions = serviceProvider.GetRequiredService<IOptions<RatesApiOptions>>().Value
                ?? throw new InvalidOperationException($"Failed to load required Rates API configuration. Ensure the {RatesApiOptions.RatesApi} section is correctly defined in appsettings.json and available in the DI container");

            if (string.IsNullOrWhiteSpace(ratesApiOptions.BaseUri))
                throw new InvalidOperationException("Rates API BaseUri is missing in configuration");

            httpClient.BaseAddress = new Uri(ratesApiOptions.BaseUri);

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        };

    private static void AddRatesApiOptions(this IServiceCollection services, IConfiguration configuration)
        => services.AddOptions<RatesApiOptions>()
                   .Bind(configuration.GetSection(RatesApiOptions.RatesApi));

    public static void AddRatesApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRatesApiOptions(configuration);
        services.AddRefitClient<IRatesApi>().ConfigureHttpClient(_configureRatesApiClientAction);
    }
}
