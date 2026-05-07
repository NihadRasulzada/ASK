using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class EventConfiguration : SoftDeletableEntityConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        base.Configure(builder);

        builder.ToTable("Events");

        // TPH: Event, Exhibition, Training hamısı bu cədvəli paylaşır
        builder.UseTphMappingStrategy();

        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<Event>("Event")
            .HasValue<Exhibition>("Exhibition")
            .HasValue<Training>("Training")
            .HasValue<BusinessForum>("BusinessForum");


        builder.Property("Discriminator")
            .HasMaxLength(13)
            .IsRequired();

        builder.Property(b => b.TitleAz).IsRequired();
        builder.Property(b => b.TitleEn).IsRequired();
        builder.Property(b => b.TitleRu).IsRequired();
        builder.Property(b => b.TextAz).IsRequired();
        builder.Property(b => b.TextEn).IsRequired();
        builder.Property(b => b.TextRu).IsRequired();

        builder.OwnsOne(a => a.TitleImageUrl, c => {
            c.Property(x => x.ImageURl).HasColumnName("TitleImageUrl");
            c.Property(x => x.PublicId).HasColumnName("TitlePublicId");
        });
        
        

        // Exhibition üçün StartDate / EndDate — nullable çünki TPH paylaşılan cədvəldir,
        // Event base tipinin bu field-ləri yoxdur.
        //builder.Property<DateTime?>("StartDate")
        //    .HasColumnName("StartDate")
        //    .IsRequired(false);

        //builder.Property<DateTime?>("EndDate")
        //    .HasColumnName("EndDate")
        //    .IsRequired(false);

        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
    }
}
