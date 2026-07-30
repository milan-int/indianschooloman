using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSiblingMasterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sibling_class_master",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sibling_class_master", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sibling_school_master",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sibling_school_master", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "sibling_class_master",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "KG I" },
                    { 2, 2, "KG II" },
                    { 3, 3, "Std I" },
                    { 4, 4, "Std II" },
                    { 5, 5, "Std III" },
                    { 6, 6, "Std IV" },
                    { 7, 7, "Std V" },
                    { 8, 8, "Std VI" },
                    { 9, 9, "Std VII" },
                    { 10, 10, "Std VIII" },
                    { 11, 11, "Std IX" },
                    { 12, 12, "Std X" },
                    { 13, 13, "Std XI" },
                    { 14, 14, "Std XII" }
                });

            migrationBuilder.InsertData(
                table: "sibling_school_master",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Indian School Bousher" },
                    { 2, 2, "Indian School Muscat" },
                    { 3, 3, "Indian School Darsait" },
                    { 4, 4, "Indian School Al Wadi Al Kabir" },
                    { 5, 5, "Indian School Al Ghubra" },
                    { 6, 6, "Indian School Al Seeb" },
                    { 7, 7, "Indian School Al Maabela" },
                    { 8, 8, "Indian School Al Wadi Al Kabir (International)" },
                    { 9, 9, "Indian School Al Ghubra (International)" },
                    { 10, 10, "Indian School Muscat - Afternoonshift" },
                    { 11, 11, "Indian School Darsait - Afternoonshift" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sibling_class_master");

            migrationBuilder.DropTable(
                name: "sibling_school_master");
        }
    }
}
