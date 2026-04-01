using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class DistrictRepresentativesConfiguration : BaseEntityConfiguration<DistrictRepresentatives>
{
    public override void Configure(EntityTypeBuilder<DistrictRepresentatives> builder)
    {
        base.Configure(builder);

        builder.ToTable("DistrictRepresentatives");

        builder.Property(e => e.DistrictAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DistrictEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.DistrictRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.FullNameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FullNameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.CompanyAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CompanyEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CompanyRu).IsRequired().HasMaxLength(200);
    }
}
