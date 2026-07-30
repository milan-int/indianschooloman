using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "master_grades",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grade_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    grade_display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    preference_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_grades", x => x.grade_id);
                });

            migrationBuilder.CreateTable(
                name: "grade_schools",
                columns: table => new
                {
                    school_option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grade_id = table.Column<int>(type: "int", nullable: false),
                    school_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grade_schools", x => x.school_option_id);
                    table.ForeignKey(
                        name: "FK_grade_schools_master_grades_grade_id",
                        column: x => x.grade_id,
                        principalTable: "master_grades",
                        principalColumn: "grade_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "unique_grade_school",
                table: "grade_schools",
                columns: new[] { "grade_id", "school_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_master_grades_grade_code",
                table: "master_grades",
                column: "grade_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grade_schools");

            migrationBuilder.DropTable(
                name: "master_grades");
        }
    }
}
