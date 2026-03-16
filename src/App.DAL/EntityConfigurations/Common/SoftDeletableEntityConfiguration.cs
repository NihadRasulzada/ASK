using App.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations.Common;

public class SoftDeletableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
    where TEntity : SoftDeletableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        // FIX: HasDefaultValueSql("0") əvəzinə HasDefaultValue(false) —
        //      DB-agnostic və type-safe.
        builder.Property(e => e.IsDeactive)
            .IsRequired()
            .HasDefaultValue(false);
    }
}