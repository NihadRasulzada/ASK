using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessForums");

            migrationBuilder.AddColumn<string>(
                name: "DetailImageUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailPublicId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetailImageUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DetailPublicId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "BusinessForums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CloudinaryURLId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TextAz = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAz = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TitleRu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DetailImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DetailPublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitlePublicId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessForums", x => x.Id);
                });
        }
    }
}
