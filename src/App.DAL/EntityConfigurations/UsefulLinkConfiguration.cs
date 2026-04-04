using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class UsefulLinkConfiguration : SoftDeletableEntityConfiguration<UsefulLink>
{
    public override void Configure(EntityTypeBuilder<UsefulLink> builder)
    {
        base.Configure(builder);

        builder.ToTable("UsefulLinks");

        builder.Property(e => e.TitleAz).IsRequired().HasMaxLength(300);
        builder.Property(e => e.TitleEn).IsRequired().HasMaxLength(300);
        builder.Property(e => e.TitleRu).IsRequired().HasMaxLength(300);
        builder.Property(e => e.Link).IsRequired();
    }
}
