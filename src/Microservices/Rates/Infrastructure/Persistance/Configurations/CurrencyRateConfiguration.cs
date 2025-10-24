using System.Runtime.ConstrainedExecution;
using CurrencyRates.Microservices.Rates.Domain.Entities;
using CurrencyRates.Microservices.Rates.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;

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
               .IsRequired();


    }
}