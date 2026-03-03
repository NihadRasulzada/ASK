using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
{
    public void Configure(EntityTypeBuilder<CurrencyRate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.ToTable("CurrencyRates");

        builder.Property(e => e.CurrencyCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.Rate)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(e => e.RateDate)
            .IsRequired();

        builder.HasIndex(e => new { e.CurrencyCode, e.RateDate })
            .IsUnique();
    }
}
