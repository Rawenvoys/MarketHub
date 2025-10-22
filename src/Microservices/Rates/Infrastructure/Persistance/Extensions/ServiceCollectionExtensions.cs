using CurrencyRates.Microservices.Rates.Domain.Interfaces;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(RatesDbContext.ConnectionStringName)
            ?? throw new InvalidOperationException($"Cannot find connection string '{RatesDbContext.ConnectionStringName}'.");

        services.AddDbContext<RatesDbContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<ISourceRepository, SourceRepository>();
    }
}
