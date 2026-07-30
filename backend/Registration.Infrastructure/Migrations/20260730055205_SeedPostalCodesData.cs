using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Registration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPostalCodesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "PostalCodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "PostalCodes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 2, "Bareeq Al Shatti-103", "Bareeq Al Shatti" },
                    { 3, "Markaz al Bahja-104", "Markaz al Bahja" },
                    { 4, "Central Post Office-111", "Central Post Office" },
                    { 5, "Ruwi-112", "Ruwi" },
                    { 6, "Muscat-113", "Muscat" },
                    { 7, "Jabroo-114", "Jabroo" },
                    { 8, "Madinat Al Sultan Qaboos-115", "Madinat Al Sultan Qaboos" },
                    { 9, "Mina Al Fahal-116", "Mina Al Fahal" },
                    { 10, "Al Wadi Al Kabir-117", "Al Wadi Al Kabir" },
                    { 11, "Al Harthy Complex-118", "Al Harthy Complex" },
                    { 12, "Al Amarat-119", "Al Amarat" },
                    { 13, "Qurayat-120", "Qurayat" },
                    { 14, "Al Seeb-121", "Al Seeb" },
                    { 15, "Al-Maabela -122", "Al-Maabela " },
                    { 16, "Sultan Qaboos University-123", "Sultan Qaboos University" },
                    { 17, "Al Rusayl -124", "Al Rusayl " },
                    { 18, "Muttrah-125", "Muttrah" },
                    { 19, "Oman Commercial Center-126", "Oman Commercial Center" },
                    { 20, "Khaula Hospital-127", "Khaula Hospital" },
                    { 21, "Seeb Airport-128", "Seeb Airport" },
                    { 22, "Al Murtafa-129", "Al Murtafa" },
                    { 23, "Azaiba-130", "Azaiba" },
                    { 24, "Al Hamriyah-131", "Al Hamriyah" },
                    { 25, "Al Khud-132", "Al Khud" },
                    { 26, "Al Khuwair-133", "Al Khuwair" },
                    { 27, "Jawharat Al Shati-134", "Jawharat Al Shati" },
                    { 28, "Central Salalah -211", "Central Salalah " },
                    { 29, "Quirun Hairiti-212", "Quirun Hairiti" },
                    { 30, "Teetam-213", "Teetam" },
                    { 31, "Al-Dahareez-214", "Al-Dahareez" },
                    { 32, "Al-Hafa-216", "Al-Hafa" },
                    { 33, "AlAwqadain-217", "AlAwqadain" },
                    { 34, "Taqa-218", "Taqa" },
                    { 35, "Madinat-Al - Haq-219", "Madinat-Al - Haq" },
                    { 36, "Marbat-220", "Marbat" },
                    { 37, "Tiwi Atair-221", "Tiwi Atair" },
                    { 38, "Thamrait-222", "Thamrait" },
                    { 39, "Sohar-311", "Sohar" },
                    { 40, "Al Musana a -312", "Al Musana a " },
                    { 41, "Widam-Al - Sahil-313", "Widam-Al - Sahil" },
                    { 42, "Al-Malda -314", "Al-Malda " },
                    { 43, "Al Suwaiq-315", "Al Suwaiq" },
                    { 44, "Al Bidaya-316", "Al Bidaya" },
                    { 45, "Al Awabi-317", "Al Awabi" },
                    { 46, "Al Rustaq-318", "Al Rustaq" },
                    { 47, "Saham-319", "Saham" },
                    { 48, "Barka-320", "Barka" },
                    { 49, "Al Tarif-321", "Al Tarif" },
                    { 50, "Falaj-Al - Qabail-322", "Falaj-Al - Qabail" },
                    { 51, "Nakhal-323", "Nakhal" },
                    { 52, "Shinas-324", "Shinas" },
                    { 53, "Liwa-325", "Liwa" },
                    { 54, "Al Khaboura-326", "Al Khaboura" },
                    { 55, "Sohar-Industrial-327", "Sohar-Industrial" },
                    { 56, "Burj-Al - Radah-329", "Burj-Al - Radah" },
                    { 57, "Sur-411", "Sur" },
                    { 58, "Al-Kamil & Al-Wafi -412", "Al-Kamil & Al-Wafi " },
                    { 59, "Ibra-413", "Ibra" },
                    { 60, "Masirah-414", "Masirah" },
                    { 61, "Ja alan Bani Bu Hassan-415", "Ja alan Bani Bu Hassan" },
                    { 62, "Ja alan Bani Bu Ali -416", "Ja alan Bani Bu Ali " },
                    { 63, "Wadi Bani Khalid -417", "Wadi Bani Khalid " },
                    { 64, "Sinaw-418", "Sinaw" },
                    { 65, "Al Mudhairib-419", "Al Mudhairib" },
                    { 66, "Al Mudhaibi-420", "Al Mudhaibi" },
                    { 67, "Bidiyah-421", "Bidiyah" },
                    { 68, "Al-Ashkharah-422", "Al-Ashkharah" },
                    { 69, "Samad-Al - Shan-423", "Samad-Al - Shan" },
                    { 70, "Dama-Wattaeen-424", "Dama-Wattaeen" },
                    { 71, "Sur Commercial District -425", "Sur Commercial District " },
                    { 72, "Ibri-511", "Ibri" },
                    { 73, "Al-Buraimi -512", "Al-Buraimi " },
                    { 74, "Yanqul-513", "Yanqul" },
                    { 75, "Dhank-514", "Dhank" },
                    { 76, "Al-Araqi -515", "Al-Araqi " },
                    { 77, "Al-Akhdar -Ibri 516", "Al-Akhdar -Ibri 516" },
                    { 78, "Al-Sinainah -517", "Al-Sinainah " },
                    { 79, "Mahda-518", "Mahda" },
                    { 80, "Khasab-811", "Khasab" },
                    { 81, "Bakha-812", "Bakha" },
                    { 82, "Daba-813", "Daba" },
                    { 83, "Madha-814", "Madha" },
                    { 84, "Nizwa-611", "Nizwa" },
                    { 85, "Bahla-612", "Bahla" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "PostalCodes",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "PostalCodes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
