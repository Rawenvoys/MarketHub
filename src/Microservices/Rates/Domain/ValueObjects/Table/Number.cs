using System;

namespace CurrencyRates.Microservices.Rates.Domain.ValueObjects.Table;

public class Number : ValueObject
{
    public string Value { get; init; }

    private Number(string value)
        => Value = value;

    public static Number FromValue(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Table number cannot be empty", nameof(number));

        return new Number(number);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
