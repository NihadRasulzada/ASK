using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class CommitteeConfiguration : BaseEntityConfiguration<Committee>
{
    public override void Configure(EntityTypeBuilder<Committee> builder)
    {
        base.Configure(builder);

        builder.ToTable("Committees");

        builder.Property(e => e.NameAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.NameRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.ChairmanAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.ChairmanEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.ChairmanRu).IsRequired().HasMaxLength(200);

        builder.Property(e => e.VicePresidentAz).IsRequired().HasMaxLength(200);
        builder.Property(e => e.VicePresidentEn).IsRequired().HasMaxLength(200);
        builder.Property(e => e.VicePresidentRu).IsRequired().HasMaxLength(200);
    }
}
