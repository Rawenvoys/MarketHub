using Microsoft.Extensions.Logging;

namespace CurrencyRates.Microservices.Rates.Application.Extensions;

public static class LoggingBuilderExtensions
{
    public static void AddApplication(this ILoggingBuilder logger)
    {
        logger.ClearProviders();
        logger.AddConsole();
    }
}
