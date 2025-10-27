using MarketHub.Microservices.Rates.Domain.Enums.Source;

namespace MarketHub.Microservices.Rates.Domain.ValueObjects.Source;

public class Timeframe : ValueObject
{

    private Timeframe(Year startYear, Year endYear, Month startMonth, Month endMonth)
    {
        StartYear = startYear;
        EndYear = endYear;
        StartMonth = startMonth;
        EndMonth = endMonth;
    }

    public static Timeframe Create(Year startYear, Year endYear, Month startMonth, Month endMonth)
    {
        if (startYear.Value > endYear.Value)
            throw new ArgumentException("Start year cannot be greater than end year");
        else if (startYear.Value == endYear.Value && startMonth.Value > endMonth.Value)
            throw new ArgumentException("Start month cannot be greater than end month");

        return new Timeframe(startYear, endYear, endMonth, startMonth);
    }

    public Year StartYear { get; init; }
    public Year EndYear { get; init; }
    public Month StartMonth { get; init; }
    public Month EndMonth { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartMonth;
        yield return EndMonth;
        yield return StartYear;
        yield return EndYear;
    }
}
