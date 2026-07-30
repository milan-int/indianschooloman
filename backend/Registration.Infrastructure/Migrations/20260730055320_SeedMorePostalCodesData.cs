using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMorePostalCodesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 86, "Bidbid-613", "Bidbid" },
                    { 87, "Izki-614", "Izki" },
                    { 88, "Lizgh-615", "Lizgh" },
                    { 89, "Birkat-616", "Birkat" },
                    { 90, "Al-Hamra-617", "Al-Hamra" },
                    { 91, "Adam-618", "Adam" },
                    { 92, "Manah-619", "Manah" },
                    { 93, "Samail-620", "Samail" },
                    { 94, "Jabal Al-Akhdar-621", "Jabal Al-Akhdar" },
                    { 95, "Haima-711", "Haima" },
                    { 96, "Al Jazr-712", "Al Jazr" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 96);
        }
    }
}
