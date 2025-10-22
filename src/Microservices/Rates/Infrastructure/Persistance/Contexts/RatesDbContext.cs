using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;

public class RatesDbContext(DbContextOptions<RatesDbContext> options) : DbContext(options)
{
    public DbSet<Source> Sources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SourceConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
