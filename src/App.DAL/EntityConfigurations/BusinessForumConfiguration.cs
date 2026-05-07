using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class BusinessForumConfiguration : IEntityTypeConfiguration<BusinessForum>
{
    //public override void Configure(EntityTypeBuilder<BusinessForum> builder)
    //{
    //    builder.HasBaseType<Event>();

        
    //}
    public void Configure(EntityTypeBuilder<BusinessForum> builder)
    {
        // TPH leaf type — öz cədvəli yoxdur, Events cədvəlini paylaşır.
        // Discriminator dəyəri "Exhibition" EventConfiguration-da təyin edilib.
        builder.HasBaseType<Event>();

        builder.OwnsOne(a => a.DetailImageUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("DetailImageUrl");
            c.Property(x => x.PublicId).HasColumnName("DetailPublicId");
        });
    }
}
