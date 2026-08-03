using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeparatePortalTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PortalLinksMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PortalConfigsMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfigValue = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalConfigsMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalGuidelinesMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LinkText = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalGuidelinesMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortalSchoolsMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlNo = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Syllabus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Classes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalSchoolsMaster", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PortalConfigsMaster",
                columns: new[] { "Id", "ConfigKey", "ConfigValue", "CreatedAt", "Description", "IsActive", "IsDeleted", "Section", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "HelplinePhone", "+968 2470 2567 / 2479 9700", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Primary Admission Helpline Phone Numbers", true, false, "CONTACT", null },
                    { 2, "HelplineEmail", "admissions@indianschoolsoman.com", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Official Admission Support Email", true, false, "CONTACT", null },
                    { 3, "OfficeHours", "Sunday to Thursday (8:00 AM – 2:00 PM)", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Helpdesk Office Timings", true, false, "CONTACT", null },
                    { 4, "AcademicYear", "2026–2027", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Current Admission Academic Year", true, false, "GENERAL", null },
                    { 5, "RegistrationFee", "OMR 15/-", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Non-refundable application processing fee", true, false, "GENERAL", null }
                });

            migrationBuilder.InsertData(
                table: "PortalGuidelinesMaster",
                columns: new[] { "Id", "CreatedAt", "Detail", "DisplayOrder", "IsActive", "IsDeleted", "Link", "LinkText", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "This online registration form is meant for Indian Nationals seeking new admissions in Indian Schools in the capital area for the academic year 2026-2027.", 1, true, false, null, null, "Eligibility", null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Online registration is mandatory. There is only one application form required for one child; our system will not accept duplicate passport entries.", 2, true, false, null, null, "Single Mandatory Application", null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A unique login registration number and password will be generated automatically upon submission and sent to your registered email and mobile number.", 3, true, false, null, null, "Credentials & Notifications", null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A non-refundable processing fee of OMR 15/- is payable upon successful submission of the application form.", 4, true, false, null, null, "Application Processing Fee", null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Online application is mandatory even for sibling admissions. To claim sibling preference, the parent must select the sibling's school as their First Preference.", 5, true, false, null, null, "Sibling Preference Rule", null },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tentative vacancies across different schools are dynamically updated on the portal for parents to review before submitting preferences.", 6, true, false, null, null, "Seat Vacancies", null },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "School allotment is strictly subject to vacancy availability and merit criteria set by the Board of Directors.", 7, true, false, null, null, "Admission Allotment", null },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Parents are strongly advised to check the Frequently Asked Questions (FAQs) section for guidance on common registration questions.", 8, true, false, null, null, "Help & Queries", null },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Parents seeking inter-school transfer for their wards must complete the dedicated transfer portal:", 9, true, false, "https://forms.gle/P29avN2BoVufqWGz5", "Inter-School Transfer Form", "Inter-School Transfer", null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Parents of non-Indian nationalities seeking admission in Indian schools must apply through the external foreign quota portal:", 10, true, false, "https://forms.gle/hEUAnuLePfyTveD89", "Other Nationalities Form", "Other Nationalities", null }
                });

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsDeleted",
                value: false);

            migrationBuilder.InsertData(
                table: "PortalSchoolsMaster",
                columns: new[] { "Id", "Classes", "Code", "CreatedAt", "DisplayOrder", "IsActive", "IsDeleted", "Location", "Name", "SlNo", "Syllabus", "UpdatedAt", "Website" },
                values: new object[,]
                {
                    { 1, "KG I – IX & XI", "ISM", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, true, false, "Darsait / Muscat", "Indian School Muscat", 1, "CBSE", null, "https://ismoman.com" },
                    { 2, "KG I – IX & XI", "ISD", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, true, false, "Darsait", "Indian School Darsait", 2, "CBSE", null, "https://isdoman.com" },
                    { 3, "KG I – IX & XI", "ISWK", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, true, false, "Wadi Kabir", "Indian School Al Wadi Al Kabir", 3, "CBSE", null, "https://iswkoman.com" },
                    { 4, "KG I – IX & XI", "ISWKi", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, true, false, "Wadi Kabir", "Indian School Al Wadi Al Kabir International", 4, "CAMBRIDGE", null, "https://iswkoman.com" },
                    { 5, "KG I – IX & XI", "ISG", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, true, false, "Al Ghubra", "Indian School Al Ghubra", 5, "CBSE", null, "https://isgoman.com" },
                    { 6, "KG I – IX & XI", "ISGi", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, true, false, "Al Ghubra", "Indian School Al Ghubra International", 6, "CAMBRIDGE", null, "https://isgoman.com" },
                    { 7, "KG I – IX & XI", "ISB", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, true, false, "Bousher", "Indian School Bousher", 7, "CBSE", null, "https://isboman.com" },
                    { 8, "KG I – IX & XI", "ISAS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, true, false, "Al Seeb", "Indian School Seeb", 8, "CBSE", null, "https://isseeoman.com" },
                    { 9, "KG I – IX & XI", "ISAM", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, true, false, "Al Maabela", "Indian School Maabela", 9, "CBSE", null, "https://isamoman.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortalConfigsMaster");

            migrationBuilder.DropTable(
                name: "PortalGuidelinesMaster");

            migrationBuilder.DropTable(
                name: "PortalSchoolsMaster");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PortalLinksMaster");
        }
    }
}
