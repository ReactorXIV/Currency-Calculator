using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Currency_Calculator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyCode);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyConversion",
                columns: table => new
                {
                    BaseCurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TargetCurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyConversion", x => new { x.BaseCurrencyCode, x.TargetCurrencyCode });
                    table.ForeignKey(
                        name: "FK_CurrencyConversion_Currency_BaseCurrencyCode",
                        column: x => x.BaseCurrencyCode,
                        principalTable: "Currency",
                        principalColumn: "CurrencyCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyConversion_Currency_TargetCurrencyCode",
                        column: x => x.TargetCurrencyCode,
                        principalTable: "Currency",
                        principalColumn: "CurrencyCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Currency",
                column: "CurrencyCode",
                values: new object[]
                {
                    "CAD",
                    "CHF",
                    "EUR",
                    "GBP",
                    "JPY",
                    "USD"
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Username", "PasswordHash", "PasswordSalt" },
                values: new object[] { "admin", new byte[] { 250, 138, 131, 1, 14, 241, 130, 209, 230, 5, 212, 0, 228, 157, 226, 99, 153, 138, 180, 158, 198, 14, 138, 199, 139, 142, 138, 10, 54, 40, 47, 14, 205, 113, 16, 210, 81, 164, 79, 222, 161, 126, 37, 123, 140, 148, 147, 226, 193, 178, 155, 17, 30, 92, 11, 212, 143, 190, 75, 233, 183, 0, 220, 111 }, new byte[] { 216, 178, 213, 161, 195, 240, 9, 101, 154, 255, 19, 79, 206, 249, 243, 170, 113, 131, 57, 18, 207, 95, 194, 61, 158, 148, 51, 10, 121, 38, 57, 222, 140, 237, 238, 120, 120, 154, 39, 233, 247, 147, 155, 47, 125, 90, 231, 137, 63, 90, 43, 97, 92, 108, 56, 82, 200, 124, 163, 123, 229, 232, 197, 167, 156, 201, 185, 168, 52, 84, 27, 103, 252, 12, 224, 229, 248, 197, 199, 130, 61, 131, 216, 250, 60, 205, 124, 129, 68, 96, 65, 245, 60, 82, 169, 120, 220, 190, 189, 10, 147, 249, 1, 171, 253, 0, 91, 58, 27, 59, 46, 88, 135, 124, 98, 156, 25, 123, 89, 1, 90, 123, 123, 41, 190, 11, 243, 137 } });

            migrationBuilder.InsertData(
                table: "CurrencyConversion",
                columns: new[] { "BaseCurrencyCode", "TargetCurrencyCode", "ConversionRate" },
                values: new object[,]
                {
                    { "CHF", "USD", 1.1379m },
                    { "EUR", "CHF", 1.2079m },
                    { "EUR", "GBP", 0.8731m },
                    { "EUR", "USD", 1.3764m },
                    { "GBP", "CAD", 1.5648m },
                    { "USD", "JPY", 76.7200m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyConversion_TargetCurrencyCode",
                table: "CurrencyConversion",
                column: "TargetCurrencyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyConversion");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
