using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class ChangeJourToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JourDeLaSemaine",
                table: "Disponibilites");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeLaSemaine",
                table: "Disponibilites",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce8b520e-64b6-4047-8c8d-b97e45ac40d2", "AQAAAAEAACcQAAAAED59W0LY5j3espPVxvDkIbIGEXlmOCs7F561p0p0atoX7nqXTQ3/v1V5aBDF2581pw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeLaSemaine",
                table: "Disponibilites");

            migrationBuilder.AddColumn<string>(
                name: "JourDeLaSemaine",
                table: "Disponibilites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bda46d60-c900-47e4-ab90-044f9a92b029", "AQAAAAEAACcQAAAAEOhc6wgopt/RP0VBTrv0NJJqSs3YnmjGUAYt5kavAsYHSe1XQzYDf8RHFHldnUdJpA==" });
        }
    }
}
