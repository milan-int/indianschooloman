using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeMasterTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GradeDisplay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreferenceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeSchools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeId = table.Column<int>(type: "int", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSchools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeSchools_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "GradeCode", "GradeDisplay", "PreferenceType" },
                values: new object[,]
                {
                    { 1, "LKG_HKG", "LKG & HKG", "Multi-Choice Preference" },
                    { 2, "STD_1", "Standard 1", "Regular Preference" },
                    { 3, "STD_9", "Standard 9", "Regular Preference" }
                });

            migrationBuilder.InsertData(
                table: "GradeSchools",
                columns: new[] { "Id", "GradeId", "SchoolName" },
                values: new object[,]
                {
                    { 1, 1, "Indian School Muscat" },
                    { 2, 1, "Indian School Darsait" },
                    { 3, 2, "Indian School Muscat" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeSchools_GradeId_SchoolName",
                table: "GradeSchools",
                columns: new[] { "GradeId", "SchoolName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeSchools");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}

