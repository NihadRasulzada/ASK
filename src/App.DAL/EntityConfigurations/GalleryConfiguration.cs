using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: BaseEntityConfiguration-dan inherit edir
public class GalleryConfiguration : BaseEntityConfiguration<Gallery>
{
    public override void Configure(EntityTypeBuilder<Gallery> builder)
    {
        base.Configure(builder);

        builder.ToTable("Galleries");


        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });
    }
}