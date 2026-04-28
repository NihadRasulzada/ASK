using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class UserPeriodSettingConfiguration : IEntityTypeConfiguration<UserPeriodSetting>
{
    public void Configure(EntityTypeBuilder<UserPeriodSetting> builder)
    {
        builder.ToTable("UserPeriodSettings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SelectedPeriod)
            .IsRequired()
            .HasConversion<string>(); // DB-də "Day", "Week", "Month" kimi saxlanır

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
