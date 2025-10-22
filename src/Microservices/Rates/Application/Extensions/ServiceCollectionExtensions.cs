using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CurrencyRates.Microservices.Rates.Infrastructure.Extensions;

namespace CurrencyRates.Microservices.Rates.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
