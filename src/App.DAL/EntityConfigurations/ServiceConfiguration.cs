using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: SoftDeletableEntityConfiguration-dan inherit edir
public class ServiceConfiguration : SoftDeletableEntityConfiguration<Service>
{
    public override void Configure(EntityTypeBuilder<Service> builder)
    {
        base.Configure(builder);

        builder.ToTable("Services");

        builder.Property(e => e.ImageUrl).IsRequired();

        builder.Property(e => e.NameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.NameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.ActivateAt).IsRequired();
    }
}