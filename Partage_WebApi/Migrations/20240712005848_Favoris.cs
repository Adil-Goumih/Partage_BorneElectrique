using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class Favoris : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favoris",
                columns: table => new
                {
                    FavorisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsFavoris = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BorneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoris", x => x.FavorisId);
                    table.ForeignKey(
                        name: "FK_Favoris_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoris_Bornes_BorneId",
                        column: x => x.BorneId,
                        principalTable: "Bornes",
                        principalColumn: "BorneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d42a61ff-0ca3-4549-8273-c2d06632a6d9", "AQAAAAEAACcQAAAAEF+nzQxnNZac4rFRVQ7Uj+YTATenDInf0gSsSrPaXvL2LTQlKCso8wakKbvVd7mNNg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Favoris_BorneId",
                table: "Favoris",
                column: "BorneId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoris_UserId",
                table: "Favoris",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoris");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "923bc27b-e88e-48b8-b135-1acbdf529f17", "AQAAAAEAACcQAAAAEL8B34V1MIcsBS1JPLmeyRIVwD0LrzS7uV33AMgM1tqkI3zLQBnAVYpqtC/g4mltfg==" });
        }
    }
}
