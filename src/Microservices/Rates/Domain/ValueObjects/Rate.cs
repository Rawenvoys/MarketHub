namespace CurrencyRates.Microservices.Rates.Domain.ValueObjects;

public class Rate : ValueObject
{
    private Rate(decimal value) 
        => Value = value;

    public static Rate FromDecimal(decimal rate)
    {
        if (rate <= 0)
            throw new ArgumentException("Exchange rate value must be positive.", nameof(rate));

        return new Rate(rate);
    }

    public decimal Value { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}