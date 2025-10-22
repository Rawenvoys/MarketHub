using Ardalis.SmartEnum;

namespace CurrencyRates.Microservices.Rates.Domain.Enums;

public sealed class SynchronizationStrategy : SmartEnum<SynchronizationStrategy, string>
{
    private SynchronizationStrategy(string value, string name) : base(name, value) { }

}
