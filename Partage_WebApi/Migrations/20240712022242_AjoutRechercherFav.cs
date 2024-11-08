using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class AjoutRechercherFav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e2ec2b49-5c1d-4887-8a80-36ed250ebd78", "AQAAAAEAACcQAAAAEAe97lcka5sT+c1hM1D7QuHcVuL5lvfEZU0nw1T4ZSgEyo9SiCdXv7TU79ygboF3Dg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d42a61ff-0ca3-4549-8273-c2d06632a6d9", "AQAAAAEAACcQAAAAEF+nzQxnNZac4rFRVQ7Uj+YTATenDInf0gSsSrPaXvL2LTQlKCso8wakKbvVd7mNNg==" });
        }
    }
}
