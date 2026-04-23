using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Services",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "PdfUrl",
                table: "Publications",
                newName: "TitlePublicId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Presidium",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Presidents",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Partners",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "OurValues",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "NewsImages",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "IconUrl",
                table: "InternationalSolidarity",
                newName: "TitlePublicId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Galleries",
                newName: "TitleImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Directors",
                newName: "TitleImageUrl");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Settings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Publications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PdfImageUrl",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PdfPublicId",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Presidium",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Presidium",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Presidents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Presidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Partners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "OurValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "OurValues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "NewsImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "NewsImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "News",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "InternationalSolidarity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitleImageUrl",
                table: "InternationalSolidarity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Galleries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Directors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "BusinessForums",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DetailPublicId",
                table: "BusinessForums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "BusinessForums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CloudinaryURLId",
                table: "Announcements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PdfImageUrl",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PdfPublicId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Presidium");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Presidium");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Presidents");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Presidents");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "OurValues");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "OurValues");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "NewsImages");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "NewsImages");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "InternationalSolidarity");

            migrationBuilder.DropColumn(
                name: "TitleImageUrl",
                table: "InternationalSolidarity");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "BusinessForums");

            migrationBuilder.DropColumn(
                name: "DetailPublicId",
                table: "BusinessForums");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "BusinessForums");

            migrationBuilder.DropColumn(
                name: "CloudinaryURLId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Services",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitlePublicId",
                table: "Publications",
                newName: "PdfUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Presidium",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Presidents",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Partners",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "OurValues",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "NewsImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitlePublicId",
                table: "InternationalSolidarity",
                newName: "IconUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Galleries",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "TitleImageUrl",
                table: "Directors",
                newName: "ImageUrl");
        }
    }
}
