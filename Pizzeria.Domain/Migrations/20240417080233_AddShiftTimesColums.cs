using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftTimesColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "shift_end_time",
                table: "shifts",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(22, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "shift_start_time",
                table: "shifts",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(8, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shift_end_time",
                table: "shifts");

            migrationBuilder.DropColumn(
                name: "shift_start_time",
                table: "shifts");
        }
    }
}
