using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class NewsConfiguration : SoftDeletableEntityConfiguration<News>
{
    public override void Configure(EntityTypeBuilder<News> builder)
    {
        base.Configure(builder);

        builder.ToTable("News");

        builder.Property(n => n.TitleImageUrl)
            .IsRequired();

        builder.Property(n => n.NewsTextAz)
            .IsRequired();

        // IEnumerable<string> — EF Core 8+ PrimitiveCollection API ilə
        // nvarchar(max) kolonunda JSON array kimi saxlanılır.
        builder.PrimitiveCollection(n => n.ImageUrls)
            .IsRequired();
    }
}
