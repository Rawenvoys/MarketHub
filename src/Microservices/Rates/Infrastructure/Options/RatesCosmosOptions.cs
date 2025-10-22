using System;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Options;

public class RatesCosmosOptions
{
    public const string RatesCosmos = nameof(RatesCosmos);
    public string ConnectionString { get; set; } = default!;
    public string DatabaseId { get; set; } = default!;
    public IEnumerable<string> ContainerNames { get; set; } = default!;
}





//AccountEndpoint=https://cdbacc-markethub-test-plcentral.documents.azure.com:443/;AccountKey=r7w2PMDIOC6dzTiPvmuDPh0oirKKNhmynUXyQwa2TVpb2yAxWVzPm2kWcYb0kdx2G1lTQ0bMmuaUACDbOwnhtA==