using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using TableType = CurrencyRates.Microservices.Rates.Domain.Enums.Table.Type;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Configurations;

public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.ToTable("Table", "rates");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.Type)
        .HasConversion(s => s.Value, s => TableType.FromValue(s))
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.Number)
               .HasConversion(s => s.Value, s => Domain.ValueObjects.Table.Number.FromValue(s))
               .HasColumnName("Number")
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.EffectiveDate)
               .IsRequired()
               .HasColumnType("date");

        // Configure currencies as a child collection (assumes Currency is an entity or owned type)
        builder.HasMany(p => p.CurrencyRates)
               .WithOne()
               .HasForeignKey("TableId")
               .OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(p => p.Source)
               .WithMany(s => s.Tables)
               .HasForeignKey(p => p.SourceId)
               .OnDelete(DeleteBehavior.Cascade);

        // Helpful indexes for typical queries
        builder.HasIndex(p => new { p.Type, p.Number });
        builder.HasIndex(p => p.EffectiveDate);
    }
}
