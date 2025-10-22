using Ardalis.SmartEnum;

namespace CurrencyRates.Microservices.Rates.Domain.Enums.Source;

public sealed class DataFetchStrategy : SmartEnum<DataFetchStrategy, string>
{
    private DataFetchStrategy(string value) : base(value, value) { }

    public readonly DataFetchStrategy NbpApiDateRangeFetchStrategy = new(nameof(NbpApiDateRangeFetchStrategy));
}
