using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StandardizePrimaryKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RegistrationId",
                table: "Registrations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Parents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DetailId",
                table: "ApplicationDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Addresses",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Registrations",
                newName: "RegistrationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Parents",
                newName: "ParentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApplicationDetails",
                newName: "DetailId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Addresses",
                newName: "AddressId");
        }
    }
}

