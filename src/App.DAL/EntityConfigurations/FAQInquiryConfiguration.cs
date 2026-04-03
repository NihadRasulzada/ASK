using App.Core.Entities;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class FAQInquiryConfiguration : BaseEntityConfiguration<FAQInquiry>
{
    public override void Configure(EntityTypeBuilder<FAQInquiry> builder)
    {
        base.Configure(builder);

        builder.ToTable("FAQInquiries");

        builder.Property(f => f.Question).IsRequired().HasMaxLength(1000);
        builder.Property(f => f.SubmittedAt).IsRequired();
    }
}
