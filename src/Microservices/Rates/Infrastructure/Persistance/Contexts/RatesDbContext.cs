using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Entities;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;

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
