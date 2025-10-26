namespace MarketHub.Microservices.Rates.Domain.ValueObjects.Source;

public class Name : ValueObject
{
    public string Value { get; init; }


    private Name(string value)
        => Value = value;

    public static Name FromValue(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Source name cannot be empty");

        return new Name(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
