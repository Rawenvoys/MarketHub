using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Enums.Source;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;

public class SourceConfiguration : IEntityTypeConfiguration<Source>
{
    public void Configure(EntityTypeBuilder<Source> builder)
    {
        builder.ToTable("Source", "rates");
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                       .HasColumnName("Name")
                       .HasMaxLength(255)
                       .IsRequired();
        });
        builder.OwnsOne(s => s.CronExpression, cronBuilder =>
            {
                cronBuilder.Property(c => c.ToString())
                           .HasColumnName("CronExpression")
                           .HasMaxLength(50)
                           .IsRequired();
            });

        builder.Property(s => s.SyncStrategy)
               .HasColumnName("SyncStrategy")
               .HasConversion(s => s.Name, s => SyncStrategy.FromValue(s))
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(s => s.Status)
               .HasColumnName("Status")
               .HasConversion(s => s.Value, s => Status.FromValue(s))
               .IsRequired();
    }
}
