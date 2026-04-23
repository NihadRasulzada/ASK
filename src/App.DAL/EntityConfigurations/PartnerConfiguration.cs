using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: BaseEntityConfiguration-dan inherit edir
public class PartnerConfiguration : BaseEntityConfiguration<Partner>
{
    public override void Configure(EntityTypeBuilder<Partner> builder)
    {
        base.Configure(builder);

        builder.ToTable("Partners");


        builder.Property(p => p.Site).IsRequired();

        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("TitleImageUrl");
            c.Property(x => x.PublicId).HasColumnName("TitlePublicId");
        });
    }
}