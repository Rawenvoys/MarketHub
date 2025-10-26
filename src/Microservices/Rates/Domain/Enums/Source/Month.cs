using Ardalis.SmartEnum;

namespace MarketHub.Microservices.Rates.Domain.Enums.Source;

public sealed class Month : SmartEnum<Month>
{
    public static readonly Month January = new(1, nameof(January));
    public static readonly Month February = new(2, nameof(February));
    public static readonly Month March = new(3, nameof(March));
    public static readonly Month April = new(4, nameof(April));
    public static readonly Month May = new(5, nameof(May));
    public static readonly Month June = new(6, nameof(June));
    public static readonly Month July = new(7, nameof(July));
    public static readonly Month August = new(8, nameof(August));
    public static readonly Month September = new(9, nameof(September));
    public static readonly Month October = new(10, nameof(October));
    public static readonly Month November = new(11, nameof(November));
    public static readonly Month December = new(12, nameof(December));

    private Month(int value, string name) : base(name, value) { }

    public static new Month FromValue(int value)
        => value switch
        {
            1 => January,
            2 => February,
            3 => March,
            4 => April,
            5 => May,
            6 => June,
            7 => July,
            8 => August,
            9 => September,
            10 => October,
            11 => November,
            12 => December,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid month value"),
        };

    public static Month FromDateOnly(DateOnly date)
        => FromValue(date.Month);
}
