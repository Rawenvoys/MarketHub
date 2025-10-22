using Ardalis.SmartEnum;
namespace CurrencyRates.Microservices.Rates.Domain.Enums;

public sealed class Type : SmartEnum<Type, string>
{
    private readonly string _description;

    public static readonly Type A = new(nameof(A), "Tabela A kursów średnich walut obcych");
    public static readonly Type B = new(nameof(B), "Tabela B kursów średnich walut obcych");
    public static readonly Type C = new(nameof(C), "Tabela C kursów kupna i sprzedaży walut obcych");

    private Type(string value, string description) : base(value, value)
        => _description = description;
}
