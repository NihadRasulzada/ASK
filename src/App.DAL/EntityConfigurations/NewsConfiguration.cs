using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: BaseEntityConfiguration-dan inherit edir
// FIX: PrimitiveCollection silindi — NewsImage entity-si istifadə olunur
public class NewsConfiguration : SoftDeletableEntityConfiguration<News>
{
    public override void Configure(EntityTypeBuilder<News> builder)
    {
        base.Configure(builder);

        builder.ToTable("News");

        builder.Property(n => n.TitleImageUrl).IsRequired();
        builder.Property(n => n.NewsTextAz).IsRequired();
        builder.Property(n => n.NewsTextEn).IsRequired();
        builder.Property(n => n.NewsTextRu).IsRequired();

        builder.HasMany(n => n.Images)
            .WithOne(i => i.News)
            .HasForeignKey(i => i.NewsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}