using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class ManagementConfiguration : BaseEntityConfiguration<Management>
{
    public override void Configure(EntityTypeBuilder<Management> builder)
    {
        base.Configure(builder);

        builder.ToTable("Management");

        builder.Property(e => e.FullNameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.CompanyAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CompanyEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CompanyRu).IsRequired().HasMaxLength(200);
    }
}
