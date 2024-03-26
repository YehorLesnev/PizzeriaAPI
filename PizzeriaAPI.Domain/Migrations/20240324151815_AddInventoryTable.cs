using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    inventory_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.inventory_id);
                    table.ForeignKey(
                        name: "FK_Inventory_recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipe",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_RecipeId",
                table: "Inventory",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
