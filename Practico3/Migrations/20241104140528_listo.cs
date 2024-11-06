using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practico3.Migrations
{
    public partial class listo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAsignacion",
                table: "Asignaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 4, 11, 5, 28, 470, DateTimeKind.Local).AddTicks(9739),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 10, 29, 23, 3, 59, 80, DateTimeKind.Local).AddTicks(3458));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAsignacion",
                table: "Asignaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 29, 23, 3, 59, 80, DateTimeKind.Local).AddTicks(3458),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 4, 11, 5, 28, 470, DateTimeKind.Local).AddTicks(9739));
        }
    }
}
