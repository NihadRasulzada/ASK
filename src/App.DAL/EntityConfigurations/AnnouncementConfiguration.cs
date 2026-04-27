using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: BaseEntityConfiguration-dan inherit edir — key setup təkrarlanmır
public class AnnouncementConfiguration : BaseEntityConfiguration<Announcement>
{
    public override void Configure(EntityTypeBuilder<Announcement> builder)
    {
        base.Configure(builder);

        builder.ToTable("Announcements");

        builder.Property(a => a.TitleAz)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.TitleEn)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.TitleRu)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.TextAz)
            .IsRequired();

        builder.Property(a => a.TextEn)
            .IsRequired();

        builder.Property(a => a.TextRu)
            .IsRequired();

        builder.OwnsOne(a => a.TitleImageUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("TitleImageUrl");
            c.Property(x => x.PublicId).HasColumnName("TitlePublicId");
        });

    }
}