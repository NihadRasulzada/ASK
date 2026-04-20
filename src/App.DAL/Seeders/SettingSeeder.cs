using System;
using System.Collections.Generic;
using System.Text;
using App.Core.Entities;
using App.Core.Enums;
using App.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Seeders;


public static class SettingSeeder
{
    private static  readonly List<Setting> DefaultSettings =
    [
        new Setting(
            Guid.Parse("11111111-0001-0000-0000-000000000000"),
            "BasKollektivSazis",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0002-0000-0000-000000000000"),
            "AzRespublikasininKonstitutsiyasi",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0003-0000-0000-000000000000"),
            "QeyriHokumetteshkilatlariHaqqindaQanun",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0004-0000-0000-000000000000"),
            "AzRespublikasiEmekMecellesi",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0005-0000-0000-000000000000"),
            "AzRespublikasiVergiMecellesi",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0006-0000-0000-000000000000"),
            "AzRespublikasiMulkiMecellesi",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0007-0000-0000-000000000000"),
            "KomissiyaHaqqinda",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0008-0000-0000-000000000000"),
            "KomissiyaninEsasnamesi",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0009-0000-0000-000000000000"),
            "KonfederasiyaHaqqinda",
            SettingValueType.Text
        ),

        new Setting(
            Guid.Parse("11111111-0010-0000-0000-000000000000"),
            "Nizamname",
            SettingValueType.Link
        ),

        new Setting(
            Guid.Parse("11111111-0011-0000-0000-000000000000"),
            "Missiyamiz",
            SettingValueType.Text
        )
    ];

    public static async Task SeedAsync(AppDbContext context)
    {
        var existingKeys = await context.Settings
            .IgnoreQueryFilters()
            .Select(x => x.Key)
            .ToListAsync();

        foreach (var setting in DefaultSettings)
        {
            if (!existingKeys.Contains(setting.Key))
            {
                await context.Settings.AddAsync(setting);
            }
        }

        await context.SaveChangesAsync();
    }
}

