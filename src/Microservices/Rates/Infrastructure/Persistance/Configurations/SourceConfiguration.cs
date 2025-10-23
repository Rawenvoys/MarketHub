using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Source;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;

public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
       public void Configure(EntityTypeBuilder<Source> builder)
       {
              builder.ToTable("Source", "rates");
              builder.HasKey(p => p.Id);
              builder.Property(p => p.Id)
                     .ValueGeneratedOnAdd();

              builder.Property(n => n.Name)
                     .HasColumnName("Name")
                     .HasConversion(s => s.Value, s => Name.FromValue(s))
                     .HasMaxLength(255)
                     .IsRequired();

              builder.Property(s => s.Cron)
                     .HasConversion<CronExpressionConverter>()
                     .HasColumnName("Cron")
                     .HasMaxLength(100)
                     .IsRequired();

              builder.Property(s => s.SyncStrategy)
                     .HasColumnName("SyncStrategy")
                     .HasConversion(s => s.Value, s => SyncStrategy.FromValue(s))
                     .HasMaxLength(50)
                     .IsRequired();

              builder.Property(s => s.Status)
                     .HasColumnName("Status")
                     .HasConversion(s => s.Value, s => Status.FromValue(s))
                     .IsRequired();
       }
}
