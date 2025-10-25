using System.Runtime.ConstrainedExecution;
using MarketHub.Microservices.Rates.Domain.Entities;
using MarketHub.Microservices.Rates.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketHub.Microservices.Rates.Infrastructure.Persistance.Configurations;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.ToTable("CurrencyRate", "rates");
        builder.HasKey(cr => cr.Id);
        builder.Property(cr => cr.Id)
               .ValueGeneratedOnAdd();

        builder.Property(cr => cr.Mid)
               .HasConversion(r => r.Value, r => Rate.FromDecimal(r))
               .HasColumnName("Mid")
               .HasColumnType("decimal(18, 6)")
               .IsRequired();
    }
}