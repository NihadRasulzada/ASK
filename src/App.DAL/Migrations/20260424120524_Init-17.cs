using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Events",
                newName: "TitleRu");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Events",
                newName: "TitleEn");

            migrationBuilder.AddColumn<string>(
                name: "TextAz",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextEn",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextRu",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAz",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextAz",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TextEn",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TextRu",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TitleAz",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TitleRu",
                table: "Events",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "Events",
                newName: "Text");
        }
    }
}
