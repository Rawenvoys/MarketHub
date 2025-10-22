using System.Text.RegularExpressions;
namespace CurrencyRates.Microservices.Rates.Domain.ValueObjects.Currency;

public partial class Code : ValueObject
{
    public string Value { get; init; }

    private Code(string value)
        => Value = value;

    public static Code FromValue(string code)
    {
        string convertedCode = Validate(code);
        return new Code(convertedCode);
    }

    private static string Validate(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Currency code cannot be empty.", nameof(code));

        var convertedCode = code.Trim().ToUpperInvariant();

        if (convertedCode.Length != 3 || !ThreeUpperLettersRegex().IsMatch(convertedCode))
            throw new ArgumentException($"Currency code '{code}' is invalid. Currency code must contains only 3 upper letters.", nameof(code));
        return convertedCode;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    [GeneratedRegex(@"^[A-Z]{3}$")]
    private static partial Regex ThreeUpperLettersRegex();
}
