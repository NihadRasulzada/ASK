using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Settings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Settings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0001-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Baş Kollektiv Sazis");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0002-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Azərbaycan Respublikasının Konstitusiyası");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0003-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Qeyri-hökumət təşkilatları haqqında qanun");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0004-0000-0000-000000000000"),
                column: "DisplayName",
                value: "AZƏRBAYCAN RESPUBLİKASININ ƏMƏK MƏCƏLLƏSİ");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0005-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Azərbaycan Respublikasının Vergi Məcəlləsi");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0006-0000-0000-000000000000"),
                column: "DisplayName",
                value: "AZƏRBAYCAN RESPUBLİKASININ MÜLKİ MƏCƏLLƏSİ");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0007-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Komissiya Haqqında");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0008-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Komissiyanın Əsasnaməsi");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0009-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Konfederasiya haqqında");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0010-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Nizamnamə");

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0011-0000-0000-000000000000"),
                column: "DisplayName",
                value: "Missiyamız");
        }
    }
}
