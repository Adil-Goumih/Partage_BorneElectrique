using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class Ajout_NCivic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "09c1f115-6b44-442e-9dc9-66b7bcd71dfe", "AQAAAAEAACcQAAAAEONUkWe4nE6CQGUbR5R7BF7UILauZu3jKtynOgyZj2IhAymxvdcDaaB0+787thuGdg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c30f7306-c6f8-4cb6-b55b-e073acbcb8b2", "AQAAAAEAACcQAAAAEDlKRrmjxvCWEX3be7sMckVWgxw4gI+WYMIr75zstFFUa7rHdJvPdvuagc1duOYCjw==" });
        }
    }
}
