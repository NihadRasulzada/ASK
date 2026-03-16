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

        builder.Property(p => p.ImageUrl).IsRequired();

        builder.Property(p => p.Site).IsRequired();
    }
}