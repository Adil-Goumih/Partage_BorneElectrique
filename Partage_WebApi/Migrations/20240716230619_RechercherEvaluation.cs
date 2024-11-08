using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class RechercherEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c30f7306-c6f8-4cb6-b55b-e073acbcb8b2", "AQAAAAEAACcQAAAAEDlKRrmjxvCWEX3be7sMckVWgxw4gI+WYMIr75zstFFUa7rHdJvPdvuagc1duOYCjw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "03652f62-c0f4-4763-ae3d-e7d28ffb6f8d", "AQAAAAEAACcQAAAAED/WsBmnhE0mX8il83kL7VQ4dLIRS9yBnaFhoT/LdWVpf+6KPQLrF+fzUrr4Tv0Mkw==" });
        }
    }
}
