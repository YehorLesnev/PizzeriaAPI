using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftAndShiftStaffTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "shift_id",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    shift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    shift_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shifts", x => x.shift_id);
                });

            migrationBuilder.CreateTable(
                name: "shift_staff",
                columns: table => new
                {
                    shift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_staff", x => new { x.shift_id, x.staff_id });
                    table.ForeignKey(
                        name: "FK_shift_staff_shift",
                        column: x => x.shift_id,
                        principalTable: "shifts",
                        principalColumn: "shift_id");
                    table.ForeignKey(
                        name: "FK_shift_staff_staff",
                        column: x => x.staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_shift_id",
                table: "orders",
                column: "shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_staff_staff_id",
                table: "shift_staff",
                column: "staff_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_shift",
                table: "orders",
                column: "shift_id",
                principalTable: "shifts",
                principalColumn: "shift_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_shift",
                table: "orders");

            migrationBuilder.DropTable(
                name: "shift_staff");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropIndex(
                name: "IX_orders_shift_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shift_id",
                table: "orders");
        }
    }
}
