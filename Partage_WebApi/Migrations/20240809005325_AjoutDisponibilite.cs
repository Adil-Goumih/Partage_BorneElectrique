using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class AjoutDisponibilite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disponibilites",
                columns: table => new
                {
                    DisponibiliteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JourDeLaSemaine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    BorneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilites", x => x.DisponibiliteId);
                    table.ForeignKey(
                        name: "FK_Disponibilites_Bornes_BorneId",
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
                values: new object[] { "fcfb73e0-3307-49ff-be3a-09ff84d80ab4", "AQAAAAEAACcQAAAAEKMCjCQ5lYmQ9j8DsJKgZHrFRnmWWyvREOJsZpUzfrO2QIBDBtEgWRDYuYH4cFPmvQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilites_BorneId",
                table: "Disponibilites",
                column: "BorneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disponibilites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3fd6fcc5-0f24-460f-a948-c31578755494", "AQAAAAEAACcQAAAAELuZdMpCvu4I3YYi8vNLN643dLidsQbWkpRCaOHQoeEW1n+mxNIYohjxHYLK9phEdg==" });
        }
    }
}
