using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: bu configuration tamamilə yox idi
public class NewsImageConfiguration : BaseEntityConfiguration<NewsImage>
{
    public override void Configure(EntityTypeBuilder<NewsImage> builder)
    {
        base.Configure(builder);

        builder.ToTable("NewsImages");


        builder.Property(i => i.NewsId).IsRequired();

        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });
    }
}