using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class ExhibitionConfiguration : IEntityTypeConfiguration<Exhibition>
{
    public void Configure(EntityTypeBuilder<Exhibition> builder)
    {
        // TPH leaf type — öz cədvəli yoxdur, Events cədvəlini paylaşır.
        // Discriminator dəyəri "Exhibition" EventConfiguration-da təyin edilib.
        builder.HasBaseType<Event>();
    }
}
