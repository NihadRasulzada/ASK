using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class BusinessForumConfiguration : BaseEntityConfiguration<BusinessForum>
{
    public override void Configure(EntityTypeBuilder<BusinessForum> builder)
    {
        base.Configure(builder);

        builder.ToTable("BusinessForums");

        builder.Property(b => b.TitleAz).IsRequired().HasMaxLength(500);
        builder.Property(b => b.TitleEn).IsRequired().HasMaxLength(500);
        builder.Property(b => b.TitleRu).IsRequired().HasMaxLength(500);
        builder.Property(b => b.TextAz).IsRequired();
        builder.Property(b => b.TextEn).IsRequired();
        builder.Property(b => b.TextRu).IsRequired();
        builder.Property(b => b.CreateDate).IsRequired();

        builder.OwnsOne(a => a.TitleImageUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("TitleImageUrl");
            c.Property(x => x.PublicId).HasColumnName("TitlePublicId");
        });

        builder.OwnsOne(a => a.DetailImageUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("DetailImageUrl");
            c.Property(x => x.PublicId).HasColumnName("DetailPublicId");
        });
    }
}
