using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMasterGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "master_grades",
                columns: new[] { "grade_id", "grade_code", "grade_display", "preference_count" },
                values: new object[,]
                {
                    { 1, "LKG_1", "LKG 1", 8 },
                    { 2, "LKG_2", "LKG 2", 8 },
                    { 3, "LKG_3", "LKG 3", 9 },
                    { 4, "STD_I", "Std I", 11 },
                    { 5, "STD_II", "Std II", 10 },
                    { 6, "STD_III", "Std III", 8 },
                    { 7, "STD_IV", "Std IV", 8 },
                    { 8, "STD_V", "Std V", 7 },
                    { 9, "STD_VI", "Std VI", 7 },
                    { 10, "STD_VII", "Std VII", 7 },
                    { 11, "STD_VIII", "Std VIII", 6 },
                    { 12, "STD_IX", "Std IX", 5 },
                    { 13, "STD_XI", "Std XI", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "master_grades",
                keyColumn: "grade_id",
                keyValue: 13);
        }
    }
}
