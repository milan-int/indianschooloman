using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "master_countries",
                columns: table => new
                {
                    country_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    country_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_countries", x => x.country_id);
                });

            migrationBuilder.InsertData(
                table: "Relationships",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 5, 5, "Brother/Sister" },
                    { 6, 6, "Twins" },
                    { 7, 7, "Triplets" }
                });

            migrationBuilder.InsertData(
                table: "master_countries",
                columns: new[] { "country_id", "country_code", "country_name" },
                values: new object[,]
                {
                    { 1, "AU", "Australia" },
                    { 2, "BD", "Bangladesh" },
                    { 3, "CA", "Canada" },
                    { 4, "IN", "India" },
                    { 5, "MY", "Malaysia" },
                    { 6, "MV", "Maldives" },
                    { 7, "NP", "Nepal" },
                    { 8, "NZ", "New Zealand" },
                    { 9, "OM", "Oman" },
                    { 10, "PK", "Pakistan" },
                    { 11, "QA", "Qatar" },
                    { 12, "SA", "Saudi Arabia" },
                    { 13, "SG", "Singapore" },
                    { 14, "ZA", "South Africa" },
                    { 15, "LK", "Sri Lanka" },
                    { 16, "SZ", "Swaziland" },
                    { 17, "AE", "United Arab Emirates" },
                    { 18, "GB", "United Kingdom" },
                    { 19, "US", "United States" },
                    { 20, "ZW", "Zimbabwe" },
                    { 21, "OTH", "Others" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_master_countries_country_code",
                table: "master_countries",
                column: "country_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_countries_country_name",
                table: "master_countries",
                column: "country_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "master_countries");

            migrationBuilder.DeleteData(
                table: "Relationships",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Relationships",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Relationships",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
