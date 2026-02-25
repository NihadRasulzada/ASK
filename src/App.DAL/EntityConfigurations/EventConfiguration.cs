using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class EventConfiguration : AuditableEntityConfiguration<Event>
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
            .HasValue<Training>("Training");

        builder.Property("Discriminator")
            .HasMaxLength(13)
            .IsRequired();

        builder.Property(e => e.Title)
            .IsRequired();

        builder.Property(e => e.TitleImageUrl)
            .IsRequired();

        builder.Property(e => e.Text)
            .IsRequired();
    }
}
