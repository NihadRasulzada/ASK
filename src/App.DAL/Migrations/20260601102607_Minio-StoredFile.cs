using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MinioStoredFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryPublicId",
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
                name: "PdfPublicId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
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
                name: "TitlePublicId",
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
                name: "DetailPublicId",
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
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "TitlePublicId",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "CloudinaryImageUrl",
                table: "Settings",
                newName: "MediaObjectKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaObjectKey",
                table: "Settings",
                newName: "CloudinaryImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryPublicId",
                table: "Settings",
                type: "nvarchar(500)",
                maxLength: 500,
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
                name: "PdfPublicId",
                table: "Publications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitlePublicId",
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
                name: "TitlePublicId",
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
                name: "DetailPublicId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

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
        }
    }
}
