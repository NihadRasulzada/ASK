using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class PublicationConfiguration : BaseEntityConfiguration<Publication>
{
    public override void Configure(EntityTypeBuilder<Publication> builder)
    {
        base.Configure(builder);

        builder.ToTable("Publications");

        builder.Property(p => p.TitleAz).IsRequired().HasMaxLength(500);
        builder.Property(p => p.TitleEn).IsRequired().HasMaxLength(500);
        builder.Property(p => p.TitleRu).IsRequired().HasMaxLength(500);

        builder.OwnsOne(a => a.TitleImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });

        builder.OwnsOne(a => a.PdfUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("PdfImageUrl");
        });


    }
}
