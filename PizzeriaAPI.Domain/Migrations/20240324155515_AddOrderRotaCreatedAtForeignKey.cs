using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderRotaCreatedAtForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddUniqueConstraint(
                name: "AK_rota_date",
                table: "rota",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_orders_created_at",
                table: "orders",
                column: "created_at");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_rota_created_at",
                table: "orders",
                column: "created_at",
                principalTable: "rota",
                principalColumn: "date",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_rota_created_at",
                table: "orders");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_rota_date",
                table: "rota");

            migrationBuilder.DropIndex(
                name: "IX_orders_created_at",
                table: "orders");

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
    }
}
