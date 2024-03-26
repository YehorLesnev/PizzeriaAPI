using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RenameInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_recipe_RecipeId",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "inventory");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_RecipeId",
                table: "inventory",
                newName: "IX_inventory_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventory",
                table: "inventory",
                column: "inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_recipe_RecipeId",
                table: "inventory",
                column: "RecipeId",
                principalTable: "recipe",
                principalColumn: "row_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_recipe_RecipeId",
                table: "inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_inventory",
                table: "inventory");

            migrationBuilder.RenameTable(
                name: "inventory",
                newName: "Inventory");

            migrationBuilder.RenameIndex(
                name: "IX_inventory_RecipeId",
                table: "Inventory",
                newName: "IX_Inventory_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "inventory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_recipe_RecipeId",
                table: "Inventory",
                column: "RecipeId",
                principalTable: "recipe",
                principalColumn: "row_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
