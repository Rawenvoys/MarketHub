using System;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Options;

public class RatesCosmosOptions
{
    public const string RatesCosmos = nameof(RatesCosmos);
    public string ConnectionString { get; set; } = default!;
    public string DatabaseId { get; set; } = default!;
    public IEnumerable<string> ContainerNames { get; set; } = default!;
}





//