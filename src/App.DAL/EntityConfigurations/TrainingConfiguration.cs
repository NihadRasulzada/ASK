using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class TrainingConfiguration : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        // TPH leaf type — öz cədvəli yoxdur, Events cədvəlini paylaşır.
        // Discriminator dəyəri "Training" EventConfiguration-da təyin edilib.
        builder.HasBaseType<Event>();
    }
}
