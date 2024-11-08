using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class miseAjourFavoris : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9dc2d4e-5d19-48a7-a23f-31d7a8c853fa", "AQAAAAEAACcQAAAAECyxboJb9WdU6DAAVgEx4KdQbfWy8LEzNpDDk2lwSdpJKor64Ni5dxmtIAZKEMniBA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "09c1f115-6b44-442e-9dc9-66b7bcd71dfe", "AQAAAAEAACcQAAAAEONUkWe4nE6CQGUbR5R7BF7UILauZu3jKtynOgyZj2IhAymxvdcDaaB0+787thuGdg==" });
        }
    }
}
