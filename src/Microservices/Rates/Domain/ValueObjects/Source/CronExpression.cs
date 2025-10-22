namespace CurrencyRates.Microservices.Rates.Domain.ValueObjects.Source;

public class CronExpression : ValueObject
{
    public string Seconds { get; init; }
    public string Minutes { get; init; }
    public string Hours { get; init; }
    public string DayOfMonth { get; init; }
    public string Month { get; init; }
    public string DayOfWeek { get; init; }

    private CronExpression(string seconds, string minutes, string hours, string dayOfMonth, string month, string dayOfWeek)
    {
        Seconds = seconds;
        Minutes = minutes;
        Hours = hours;
        DayOfMonth = dayOfMonth;
        Month = month;
        DayOfWeek = dayOfWeek;
    }

    public static CronExpression Create(string cronExpression)
    {
        if (string.IsNullOrWhiteSpace(cronExpression))
            throw new ArgumentException("Cron expression cannot be empty");
            
        var cronExpressionParts = cronExpression.Trim().Split(' ');
        if (cronExpressionParts.Length < 6 || cronExpressionParts.Length > 7)
            throw new ArgumentException("Cron expression must have minimum 6 and maximum 7 parts");

        return new CronExpression(cronExpressionParts[0], cronExpressionParts[1], cronExpressionParts[2], cronExpressionParts[3], cronExpressionParts[4], cronExpressionParts[5]);    
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Seconds;
        yield return Minutes;
        yield return Hours;
        yield return DayOfMonth;
        yield return Month;
        yield return DayOfWeek;
    }
}
