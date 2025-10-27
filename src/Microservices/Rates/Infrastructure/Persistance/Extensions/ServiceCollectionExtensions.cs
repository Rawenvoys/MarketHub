using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using MarketHub.Microservices.Rates.Infrastructure.Options;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Repositories;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(RatesDbContext.ConnectionStringName)
            ?? throw new InvalidOperationException($"Cannot find connection string '{RatesDbContext.ConnectionStringName}'");

        services.AddDbContext<RatesDbContext>(options => options.UseSqlServer(connectionString));
        // services.AddDbContext<RatesDbContext>(options
        //     => options.UseSqlite($"Data Source={Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Rates.db")}"));

        services.AddScoped<ISourceRepository, SourceRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
    }

    public static void AddRatesCosmos(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRatesCosmosClient(configuration);
        services.AddScoped<ISyncStateRepository, SyncStateRepository>();
        services.AddScoped<SyncStateSeeder>();
    }

    private static void AddRatesCosmosOptions(this IServiceCollection services, IConfiguration configuration)
            => services.AddOptions<RatesCosmosOptions>()
                       .Bind(configuration.GetSection(RatesCosmosOptions.RatesCosmos));

    private static void AddRatesCosmosClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRatesCosmosOptions(configuration);
        services.AddSingleton<CosmosClient>((serviceProvider) =>
        {
            var ratesCosmosOptions = serviceProvider.GetRatesCosmosOptions();
            return new(ratesCosmosOptions.ConnectionString, new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase }
            });
        });

        services.AddScoped<Func<string, Container>>((serviceProvider) => (containerName) =>
        {
            var ratesCosmosOptions = serviceProvider.GetRatesCosmosOptions();
            var client = serviceProvider.GetRequiredService<CosmosClient>();

            if (ratesCosmosOptions.ContainerNames == null || !ratesCosmosOptions.ContainerNames.Contains(containerName))
                throw new InvalidOperationException($"Container '{containerName}' is not defined in RatesCosmosOptions ContainerNames");

            return client.GetDatabase(ratesCosmosOptions.DatabaseId).GetContainer(containerName);
        });
    }

    private static RatesCosmosOptions GetRatesCosmosOptions(this IServiceProvider serviceProvider)
        => serviceProvider.GetRequiredService<IOptions<RatesCosmosOptions>>().Value
            ?? throw new InvalidOperationException($"Failed to load required Cosmos DB configuration. Ensure the {RatesCosmosOptions.RatesCosmos} section is correctly defined in appsettings.json and available in the DI container");
}
