using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders",
                table: "order_items");

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_items_orders",
                table: "order_items");

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_orders",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id");
        }
    }
}
