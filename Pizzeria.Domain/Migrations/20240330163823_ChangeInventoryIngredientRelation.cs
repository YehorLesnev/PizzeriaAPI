using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInventoryIngredientRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ingredients_inventory",
                table: "ingredients");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_ingredients",
                table: "ingredients",
                column: "ingredient_id",
                principalTable: "inventory",
                principalColumn: "ingredient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_ingredients",
                table: "ingredients");

            migrationBuilder.AddForeignKey(
                name: "FK_ingredients_inventory",
                table: "ingredients",
                column: "ingredient_id",
                principalTable: "inventory",
                principalColumn: "ingredient_id");
        }
    }
}
