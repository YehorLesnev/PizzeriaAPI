using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderFkOnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_address",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_staff",
                table: "orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "staff_id",
                table: "orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_address",
                table: "orders",
                column: "delivery_address_id",
                principalTable: "address",
                principalColumn: "address_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_staff",
                table: "orders",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "staff_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_address",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_staff",
                table: "orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "staff_id",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_address",
                table: "orders",
                column: "delivery_address_id",
                principalTable: "address",
                principalColumn: "address_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_staff",
                table: "orders",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "staff_id");
        }
    }
}
