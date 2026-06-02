using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class PresidiumConfiguration : BaseEntityConfiguration<Presidium>
{
    public override void Configure(EntityTypeBuilder<Presidium> builder)
    {
        base.Configure(builder);

        builder.ToTable("Presidium");

        builder.Property(e => e.FullNameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.PositionAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.PositionEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.PositionRu).IsRequired().HasMaxLength(200);


        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });
    }
}
