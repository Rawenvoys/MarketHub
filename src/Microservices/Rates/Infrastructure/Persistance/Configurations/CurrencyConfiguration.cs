using CurrencyRates.Microservices.Rates.Domain.Entities;
using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;

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

        builder.HasIndex(c => c.Code)
               .IsUnique()
               .HasDatabaseName("IX_Currency_Code");

        throw new NotImplementedException();
    }
}
