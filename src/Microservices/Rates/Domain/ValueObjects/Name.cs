
namespace CurrencyRates.Microservices.Rates.Domain.ValueObjects;

public class Name : ValueObject
{
    public string Value { get; init; }

    private Name(string value)
        => Value = value;

    public static Name FromValue(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Currency name cannot be empty");

        return new Name(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
