using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderItemsFK_order_items_itemsOnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_items_items",
                table: "order_items");

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_items",
                table: "order_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "item_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_items_items",
                table: "order_items");

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_items",
                table: "order_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "item_id");
        }
    }
}
