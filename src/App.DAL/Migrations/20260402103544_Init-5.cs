using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ChairmanAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ChairmanEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ChairmanRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VicePresidentAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VicePresidentEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VicePresidentRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictRepresentatives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DistrictEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DistrictRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictRepresentatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForeignRepresentatives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CountryEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CountryRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DutyAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DutyEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DutyRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignRepresentatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternationalSolidarity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalSolidarity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullNameAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Management", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleAz = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TitleRu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Presidium",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullNameAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PositionAz = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PositionEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PositionRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presidium", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ValueType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "DisplayName", "Key", "Value", "ValueType" },
                values: new object[,]
                {
                    { new Guid("11111111-0001-0000-0000-000000000000"), "Baş Kollektiv Sazis", "BasKollektivSazis", null, (byte)0 },
                    { new Guid("11111111-0002-0000-0000-000000000000"), "Azərbaycan Respublikasının Konstitusiyası", "AzRespublikasininKonstitutsiyasi", null, (byte)0 },
                    { new Guid("11111111-0003-0000-0000-000000000000"), "Qeyri-hökumət təşkilatları haqqında qanun", "QeyriHokumetteshkilatlariHaqqindaQanun", null, (byte)0 },
                    { new Guid("11111111-0004-0000-0000-000000000000"), "AZƏRBAYCAN RESPUBLİKASININ ƏMƏK MƏCƏLLƏSİ", "AzRespublikasiEmekMecellesi", null, (byte)0 },
                    { new Guid("11111111-0005-0000-0000-000000000000"), "Azərbaycan Respublikasının Vergi Məcəlləsi", "AzRespublikasiVergiMecellesi", null, (byte)0 },
                    { new Guid("11111111-0006-0000-0000-000000000000"), "AZƏRBAYCAN RESPUBLİKASININ MÜLKİ MƏCƏLLƏSİ", "AzRespublikasiMulkiMecellesi", null, (byte)0 },
                    { new Guid("11111111-0007-0000-0000-000000000000"), "Komissiya Haqqında", "KomissiyaHaqqinda", null, (byte)0 },
                    { new Guid("11111111-0008-0000-0000-000000000000"), "Komissiyanın Əsasnaməsi", "KomissiyaninEsasnamesi", null, (byte)0 },
                    { new Guid("11111111-0009-0000-0000-000000000000"), "Konfederasiya haqqında", "KonfederasiyaHaqqinda", null, (byte)1 },
                    { new Guid("11111111-0010-0000-0000-000000000000"), "Nizamnamə", "Nizamname", null, (byte)0 },
                    { new Guid("11111111-0011-0000-0000-000000000000"), "Missiyamız", "Missiyamiz", null, (byte)1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Key",
                table: "Settings",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "DistrictRepresentatives");

            migrationBuilder.DropTable(
                name: "ForeignRepresentatives");

            migrationBuilder.DropTable(
                name: "InternationalSolidarity");

            migrationBuilder.DropTable(
                name: "Management");

            migrationBuilder.DropTable(
                name: "OurValues");

            migrationBuilder.DropTable(
                name: "Presidium");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
