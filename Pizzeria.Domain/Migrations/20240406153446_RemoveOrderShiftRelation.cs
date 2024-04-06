using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrderShiftRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_shift",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_shift_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shift_id",
                table: "orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "shift_id",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_orders_shift_id",
                table: "orders",
                column: "shift_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_shift",
                table: "orders",
                column: "shift_id",
                principalTable: "shifts",
                principalColumn: "shift_id");
        }
    }
}
