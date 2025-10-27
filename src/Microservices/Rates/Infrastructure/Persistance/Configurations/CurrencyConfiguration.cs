using MarketHub.Microservices.Rates.Domain.Entities;
using MarketHub.Microservices.Rates.Domain.ValueObjects.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currency", "rates");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        builder.Property(c => c.Code)
               .HasConversion(c => c.Value, c => Code.FromValue(c))
               .HasColumnName("Code")
               .HasMaxLength(3)
               .IsRequired();

        builder.Property(c => c.Name)
               .HasConversion(c => c.Value, c => Name.FromValue(c))
               .HasColumnName("Name")
               .HasMaxLength(255)
               .IsRequired();

        builder.HasIndex(c => new { c.Code, c.Name })
               .IsUnique();
    }
}
