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

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.TitleImageUrl)
            .IsRequired();

        builder.Property(a => a.Text)
            .IsRequired();
    }
}