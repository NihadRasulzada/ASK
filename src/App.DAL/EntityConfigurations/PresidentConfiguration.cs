using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: BaseEntityConfiguration-dan inherit edir
public class PresidentConfiguration : BaseEntityConfiguration<President>
{
    public override void Configure(EntityTypeBuilder<President> builder)
    {
        base.Configure(builder);

        builder.ToTable("Presidents");


        builder.Property(e => e.Text).IsRequired();

        builder.OwnsOne(a => a.ImageUrl, c => {
            c.Property(x => x.ObjectKey).HasColumnName("TitleImageUrl");
        });
    }
}