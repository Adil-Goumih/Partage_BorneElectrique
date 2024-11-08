using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class SuprimFavoris : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fd6fcc5-0f24-460f-a948-c31578755494", "AQAAAAEAACcQAAAAELuZdMpCvu4I3YYi8vNLN643dLidsQbWkpRCaOHQoeEW1n+mxNIYohjxHYLK9phEdg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e9dc2d4e-5d19-48a7-a23f-31d7a8c853fa", "AQAAAAEAACcQAAAAECyxboJb9WdU6DAAVgEx4KdQbfWy8LEzNpDDk2lwSdpJKor64Ni5dxmtIAZKEMniBA==" });
        }
    }
}
