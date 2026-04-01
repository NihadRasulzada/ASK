using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class ForeignRepresentativesConfiguration : BaseEntityConfiguration<ForeignRepresentatives>
{
    public override void Configure(EntityTypeBuilder<ForeignRepresentatives> builder)
    {
        base.Configure(builder);

        builder.ToTable("ForeignRepresentatives");

        builder.Property(e => e.CountryAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CountryEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CountryRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.FullNameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.CompanyAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CompanyEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CompanyRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.DutyAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DutyEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DutyRu).IsRequired().HasMaxLength(200);
    }
}
