using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: SoftDeletableEntityConfiguration-dan inherit edir
public class DirectorConfiguration : SoftDeletableEntityConfiguration<Director>
{
    public override void Configure(EntityTypeBuilder<Director> builder)
    {
        base.Configure(builder);

        builder.ToTable("Directors");


        builder.Property(e => e.FullNameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.DutyAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DutyEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DutyRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.DepartmentAz).HasMaxLength(200);
        builder.Property(e => e.DepartmentEn).HasMaxLength(200);
        builder.Property(e => e.DepartmentRu).HasMaxLength(200);

        builder.Property(e => e.PhoneNumber).HasMaxLength(50);
        builder.Property(e => e.Email).HasMaxLength(256);

        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });
    }
}