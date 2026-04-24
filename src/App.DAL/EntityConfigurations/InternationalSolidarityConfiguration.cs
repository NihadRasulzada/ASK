using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class InternationalSolidarityConfiguration : BaseEntityConfiguration<InternationalSolidarity>
{
    public override void Configure(EntityTypeBuilder<InternationalSolidarity> builder)
    {
        base.Configure(builder);

        builder.ToTable("InternationalSolidarity");

        builder.Property(e => e.Link).IsRequired().HasMaxLength(2048);

        builder.OwnsOne(a => a.IconUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("TitleImageUrl");
            c.Property(x => x.PublicId).HasColumnName("TitlePublicId");
        });
    }
}
