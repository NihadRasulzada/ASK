using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

// FIX: BaseEntityConfiguration-dan inherit edir
public class VideoConfiguration : BaseEntityConfiguration<Video>
{
    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        builder.ToTable("Videos");

        builder.Property(v => v.Link).IsRequired();
    }
}