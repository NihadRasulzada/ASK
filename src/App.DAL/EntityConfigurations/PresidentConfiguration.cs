using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class PresidentConfiguration : IEntityTypeConfiguration<President>
{
    public void Configure(EntityTypeBuilder<President> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.ToTable("Presidents");

        builder.Property(e => e.ImageUrl)
            .IsRequired();

        builder.Property(e => e.Text)
            .IsRequired();
    }
}
