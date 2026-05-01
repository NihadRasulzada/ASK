using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key", "StringValue", "ValueType" },
                values: new object[,]
                {
                    { new Guid("11111111-0018-0000-0000-000000000000"), "Location", null, (byte)1 },
                    { new Guid("11111111-0019-0000-0000-000000000000"), "Number", null, (byte)1 },
                    { new Guid("11111111-0020-0000-0000-000000000000"), "Email", null, (byte)1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0018-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0019-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0020-0000-0000-000000000000"));
        }
    }
}
