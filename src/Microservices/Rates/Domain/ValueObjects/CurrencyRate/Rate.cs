namespace CurrencyRates.Microservices.Rates.Domain.ValueObjects;

public class Rate : ValueObject
{
    public decimal Value { get; init; }
    private Rate(decimal value)
        => Value = value;

    public static Rate FromDecimal(decimal rate)
    {
        if (rate <= 0)
            return Empty;
        return new Rate(rate);
    }

    public static Rate Empty => new(0);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}