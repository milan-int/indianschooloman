using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePortalLinksDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "LinkType", "OpenInNewTab", "TargetUrl" },
                values: new object[] { "Directory & links of all Indian Schools in Oman", "PDF_DOCUMENT", false, "assets/docs/indian_schools_websites.pdf" });

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 9,
                column: "TargetUrl",
                value: "assets/docs/annexure_a.pdf");

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 10,
                column: "TargetUrl",
                value: "assets/docs/notice_to_parents.pdf");

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 11,
                column: "TargetUrl",
                value: "assets/docs/annexure_a.pdf");

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 12,
                column: "TargetUrl",
                value: "assets/docs/indian_schools_websites.pdf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "LinkType", "OpenInNewTab", "TargetUrl" },
                values: new object[] { "Direct portals to all capital area Indian schools", "EXTERNAL_URL", true, "https://indianschoolsoman.com" });

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 9,
                column: "TargetUrl",
                value: "assets/docs/product_description.pdf");

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 10,
                column: "TargetUrl",
                value: "assets/docs/privacy_policy.pdf");

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 11,
                column: "TargetUrl",
                value: "assets/docs/delivery_policy.pdf");

            migrationBuilder.UpdateData(
                table: "PortalLinksMaster",
                keyColumn: "Id",
                keyValue: 12,
                column: "TargetUrl",
                value: "assets/docs/contact_us.pdf");
        }
    }
}
