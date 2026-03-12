using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.ToTable("Directors");

        builder.Property(e => e.ImageUrl)
            .IsRequired();

        builder.Property(e => e.FullNameAz)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.DutyAz)
            .IsRequired()
            .HasMaxLength(200);
    }
}
