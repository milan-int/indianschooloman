using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoAndBrandingConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PortalConfigsMaster",
                columns: new[] { "Id", "ConfigKey", "ConfigValue", "CreatedAt", "Description", "IsActive", "IsDeleted", "Section", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, "PortalLogoUrl", "assets/logo.png", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Primary Indian Schools Oman Portal Logo", true, false, "BRANDING", null },
                    { 7, "BrandTitle", "Indian Schools Oman", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Main Brand Title", true, false, "BRANDING", null },
                    { 8, "BrandSubTitle", "Central Admission System", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Main Brand Subtitle", true, false, "BRANDING", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PortalConfigsMaster",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PortalConfigsMaster",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PortalConfigsMaster",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
