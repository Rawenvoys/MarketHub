using MarketHub.Microservices.Rates.Domain.Aggregates;
using MarketHub.Microservices.Rates.Domain.Entities;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Configurations;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;

public class RatesDbContext(DbContextOptions<RatesDbContext> options)
    : DbContext(options)
{
    public const string ConnectionStringName = "RatesDb";
    public DbSet<Source> Sources { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }
    public DbSet<Currency> Currencies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CurrencyRateConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new SourceConfiguration());
        modelBuilder.ApplyConfiguration(new TableConfiguration());

        modelBuilder.Entity<Source>().HasData(SourceSeeder.NbpApiDateRangeSource);
    }
}
