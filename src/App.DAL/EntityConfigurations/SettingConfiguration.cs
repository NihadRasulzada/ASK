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

        builder.Property(s => s.DisplayName)
            .IsRequired()
            .HasMaxLength(500);

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
                "Baş Kollektiv Sazis",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0002-0000-0000-000000000000"),
                "AzRespublikasininKonstitutsiyasi",
                "Azərbaycan Respublikasının Konstitusiyası",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0003-0000-0000-000000000000"),
                "QeyriHokumetteshkilatlariHaqqindaQanun",
                "Qeyri-hökumət təşkilatları haqqında qanun",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0004-0000-0000-000000000000"),
                "AzRespublikasiEmekMecellesi",
                "AZƏRBAYCAN RESPUBLİKASININ ƏMƏK MƏCƏLLƏSİ",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0005-0000-0000-000000000000"),
                "AzRespublikasiVergiMecellesi",
                "Azərbaycan Respublikasının Vergi Məcəlləsi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0006-0000-0000-000000000000"),
                "AzRespublikasiMulkiMecellesi",
                "AZƏRBAYCAN RESPUBLİKASININ MÜLKİ MƏCƏLLƏSİ",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0007-0000-0000-000000000000"),
                "KomissiyaHaqqinda",
                "Komissiya Haqqında",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0008-0000-0000-000000000000"),
                "KomissiyaninEsasnamesi",
                "Komissiyanın Əsasnaməsi",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0009-0000-0000-000000000000"),
                "KonfederasiyaHaqqinda",
                "Konfederasiya haqqında",
                SettingValueType.Text),

            new Setting(
                new Guid("11111111-0010-0000-0000-000000000000"),
                "Nizamname",
                "Nizamnamə",
                SettingValueType.Link),

            new Setting(
                new Guid("11111111-0011-0000-0000-000000000000"),
                "Missiyamiz",
                "Missiyamız",
                SettingValueType.Text)
        );
    }
}
