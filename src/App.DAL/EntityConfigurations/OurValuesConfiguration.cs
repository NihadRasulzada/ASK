using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class OurValuesConfiguration : BaseEntityConfiguration<OurValues>
{
    public override void Configure(EntityTypeBuilder<OurValues> builder)
    {
        base.Configure(builder);

        builder.ToTable("OurValues");

        builder.Property(e => e.TitleAz).IsRequired().HasMaxLength(500);
        builder.Property(e => e.TitleEn).IsRequired().HasMaxLength(500);
        builder.Property(e => e.TitleRu).IsRequired().HasMaxLength(500);


        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });
    }
}
