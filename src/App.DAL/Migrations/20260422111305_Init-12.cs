using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "Value", "ValueType" },
                values: new object[,]
                {
                    { new Guid("11111111-0012-0000-0000-000000000000"), "Membership", null, (byte)1 },
                    { new Guid("11111111-0013-0000-0000-000000000000"), "HeroTitle", null, (byte)1 },
                    { new Guid("11111111-0014-0000-0000-000000000000"), "HeroDescription", null, (byte)1 },
                    { new Guid("11111111-0015-0000-0000-000000000000"), "HeroStatMemberCount", null, (byte)1 },
                    { new Guid("11111111-0016-0000-0000-000000000000"), "HeroStatPartnerCount", null, (byte)1 },
                    { new Guid("11111111-0017-0000-0000-000000000000"), "HeroStatEventCount", null, (byte)1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0012-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0013-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0014-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0015-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0016-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0017-0000-0000-000000000000"));
        }
    }
}
