using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Settings",
                newName: "StringValue");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryImageUrl",
                table: "Settings",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryPublicId",
                table: "Settings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryImageUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CloudinaryPublicId",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "StringValue",
                table: "Settings",
                newName: "Value");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Settings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0001-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0002-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0003-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0004-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0005-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0006-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0007-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0008-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0009-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0010-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0011-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0012-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0013-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0014-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0015-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0016-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0017-0000-0000-000000000000"),
                column: "CloudinaryURLId",
                value: null);
        }
    }
}
