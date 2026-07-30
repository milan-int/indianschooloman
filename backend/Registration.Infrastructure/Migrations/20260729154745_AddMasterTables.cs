using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotherTongues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherTongues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MotherTongues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bengali" },
                    { 2, "Gujarati" },
                    { 3, "Hindi" },
                    { 4, "Kannada" },
                    { 5, "Malayalam" },
                    { 6, "Marathi" },
                    { 7, "Punjabi" },
                    { 8, "Tamil" },
                    { 9, "Telugu" },
                    { 10, "Urdu" },
                    { 11, "Others" }
                });

            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1, "Muscat-100", "Muscat" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotherTongues");

            migrationBuilder.DropTable(
                name: "PostalCodes");
        }
    }
}

