using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPortalLinksMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortalLinksMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LinkType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TargetUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OpenInNewTab = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalLinksMaster", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PortalLinksMaster",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IsActive", "LinkType", "OpenInNewTab", "Section", "TargetUrl", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Register a new student for Academic Year 2026–2027", 1, true, "INTERNAL_ROUTE", false, "ADMISSION_LINK", "/register", "NEW APPLICATION", null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Important announcements and eligibility criteria", 2, true, "PDF_DOCUMENT", false, "ADMISSION_LINK", "assets/docs/notice_to_parents.pdf", "Notice to Parents", null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Direct portals to all capital area Indian schools", 3, true, "EXTERNAL_URL", true, "ADMISSION_LINK", "https://indianschoolsoman.com", "Indian Schools Websites", null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Find answers regarding admission procedures", 4, true, "PDF_DOCUMENT", false, "ADMISSION_LINK", "assets/docs/faq.pdf", "FAQ", null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Overview of 2nd & 3rd languages available per school", 5, true, "PDF_DOCUMENT", false, "ADMISSION_LINK", "assets/docs/languages_offered.pdf", "Languages offered in Schools", null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Transfer guidelines between Indian schools in Oman", 6, true, "PDF_DOCUMENT", false, "ADMISSION_LINK", "assets/docs/inter_school_transfer.pdf", "Inter-School Transfer", null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Registration guidelines for non-Indian passport holders", 7, true, "PDF_DOCUMENT", false, "ADMISSION_LINK", "assets/docs/admissions_other_nationalities.pdf", "Admissions to Other Nationalities", null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Check seat availability across all classes & schools", 8, true, "PDF_DOCUMENT", false, "ADMISSION_LINK", "assets/docs/projected_vacancies.pdf", "Projected Vacancies", null },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Portal product description", 1, true, "PDF_DOCUMENT", false, "FOOTER_LINK", "assets/docs/product_description.pdf", "Product Description", null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Privacy policy details", 2, true, "PDF_DOCUMENT", false, "FOOTER_LINK", "assets/docs/privacy_policy.pdf", "Privacy Policy", null },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delivery terms and policy", 3, true, "PDF_DOCUMENT", false, "FOOTER_LINK", "assets/docs/delivery_policy.pdf", "Delivery Policy", null },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Contact details and helpdesk information", 4, true, "PDF_DOCUMENT", false, "FOOTER_LINK", "assets/docs/contact_us.pdf", "ContactUS", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortalLinksMaster");
        }
    }
}
