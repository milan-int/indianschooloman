using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentSiblingsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentExistingSiblings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SiblingName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GrNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Division = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExistingSiblings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExistingSiblings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentNewApplicantSiblings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNewApplicantSiblings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentNewApplicantSiblings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentExistingSiblings_StudentId",
                table: "StudentExistingSiblings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNewApplicantSiblings_StudentId",
                table: "StudentNewApplicantSiblings",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExistingSiblings");

            migrationBuilder.DropTable(
                name: "StudentNewApplicantSiblings");
        }
    }
}
