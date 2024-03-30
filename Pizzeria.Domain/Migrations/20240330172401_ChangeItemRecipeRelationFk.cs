using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemRecipeRelationFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_recipes",
                table: "items");

            migrationBuilder.CreateIndex(
                name: "IX_items_recipe_id",
                table: "items",
                column: "recipe_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_items_recipes",
                table: "items",
                column: "recipe_id",
                principalTable: "recipes",
                principalColumn: "recipe_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_recipes",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_recipe_id",
                table: "items");

            migrationBuilder.AddForeignKey(
                name: "FK_items_recipes",
                table: "items",
                column: "item_id",
                principalTable: "recipes",
                principalColumn: "recipe_id");
        }
    }
}
