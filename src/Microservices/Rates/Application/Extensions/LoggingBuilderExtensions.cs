using Microsoft.Extensions.Logging;

namespace MarketHub.Microservices.Rates.Application.Extensions;

public static class LoggingBuilderExtensions
{
    public static void AddApplication(this ILoggingBuilder logger)
    {
        logger.ClearProviders();
        logger.AddConsole();
    }
}
