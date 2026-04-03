using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class FAQConfiguration : SoftDeletableEntityConfiguration<FAQ>
{
    public override void Configure(EntityTypeBuilder<FAQ> builder)
    {
        base.Configure(builder);

        builder.ToTable("FAQs");

        builder.Property(f => f.QuestionAz).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.QuestionEn).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.QuestionRu).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.AnswerAz).IsRequired();
        builder.Property(f => f.AnswerEn).IsRequired();
        builder.Property(f => f.AnswerRu).IsRequired();
    }
}
