using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderRotaDateForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rota_orders_OrderDate",
                table: "rota");

            migrationBuilder.DropIndex(
                name: "IX_rota_OrderDate",
                table: "rota");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "rota");

            migrationBuilder.AddColumn<Guid>(
                name: "RotaDate",
                table: "orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_RotaDate",
                table: "orders",
                column: "RotaDate");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_rota_RotaDate",
                table: "orders",
                column: "RotaDate",
                principalTable: "rota",
                principalColumn: "row_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_rota_RotaDate",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_RotaDate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "RotaDate",
                table: "orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDate",
                table: "rota",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_rota_OrderDate",
                table: "rota",
                column: "OrderDate");

            migrationBuilder.AddForeignKey(
                name: "FK_rota_orders_OrderDate",
                table: "rota",
                column: "OrderDate",
                principalTable: "orders",
                principalColumn: "row_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
