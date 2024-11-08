using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Partage.Migrations
{
    public partial class modifDisponibiliteString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HeureFin",
                table: "Disponibilites",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "HeureDebut",
                table: "Disponibilites",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bda46d60-c900-47e4-ab90-044f9a92b029", "AQAAAAEAACcQAAAAEOhc6wgopt/RP0VBTrv0NJJqSs3YnmjGUAYt5kavAsYHSe1XQzYDf8RHFHldnUdJpA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HeureFin",
                table: "Disponibilites",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HeureDebut",
                table: "Disponibilites",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fcfb73e0-3307-49ff-be3a-09ff84d80ab4", "AQAAAAEAACcQAAAAEKMCjCQ5lYmQ9j8DsJKgZHrFRnmWWyvREOJsZpUzfrO2QIBDBtEgWRDYuYH4cFPmvQ==" });
        }
    }
}
