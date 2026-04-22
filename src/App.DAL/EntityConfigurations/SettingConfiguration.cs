using App.Core.Entities;
using App.Core.Enums;
using App.DAL.EntityConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.DAL.EntityConfigurations;

public class SettingConfiguration : BaseEntityConfiguration<Setting>
{
    public override void Configure(EntityTypeBuilder<Setting> builder)
    {
        base.Configure(builder);

        builder.ToTable("Settings");

        builder.Property(s => s.Key)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(s => s.Key)
            .IsUnique();

        builder.Property(s => s.Value)
            .HasMaxLength(4000);

        builder.Property(s => s.ValueType)
            .IsRequired()
            .HasConversion<byte>();

        // ── Seed data — deterministic GUIDs, Value = null ─────────────────────
        builder.HasData(
            new Setting(
                new Guid("11111111-0001-0000-0000-000000000000"),
                "BasKollektivSazis",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0002-0000-0000-000000000000"),
                "AzRespublikasininKonstitutsiyasi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0003-0000-0000-000000000000"),
                "QeyriHokumetteshkilatlariHaqqindaQanun",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0004-0000-0000-000000000000"),
                "AzRespublikasiEmekMecellesi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0005-0000-0000-000000000000"),
                "AzRespublikasiVergiMecellesi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0006-0000-0000-000000000000"),
                "AzRespublikasiMulkiMecellesi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0007-0000-0000-000000000000"),
                "KomissiyaHaqqinda",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0008-0000-0000-000000000000"),
                "KomissiyaninEsasnamesi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0009-0000-0000-000000000000"),
                "KonfederasiyaHaqqinda",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0010-0000-0000-000000000000"),
                "Nizamname",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0011-0000-0000-000000000000"),
                "Missiyamiz",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0012-0000-0000-000000000000"),
                "Membership",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0013-0000-0000-000000000000"),
                "HeroTitle",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0014-0000-0000-000000000000"),
                "HeroDescription",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0015-0000-0000-000000000000"),
                "HeroStatMemberCount",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0016-0000-0000-000000000000"),
                "HeroStatPartnerCount",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0017-0000-0000-000000000000"),
                "HeroStatEventCount",
                SettingValueType.Text)


        );
    }
}
