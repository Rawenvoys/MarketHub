using System;

namespace MarketHub.Microservices.Rates.Domain.ValueObjects.Source;

public class Year : ValueObject
{
    public int Value { get; init; }

    public static Year FromValue(int value)
    {
        if (value < 0)
            throw new ArgumentException("Year cannot be negative");

        return new Year(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;

    }

    private Year(int value)
        => Value = value;
}
