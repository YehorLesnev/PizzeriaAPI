using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIngredientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    ingredient_weight = table.Column<int>(type: "int", nullable: false),
                    ingredient_measure = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ingredient_price = table.Column<decimal>(type: "Decimal(18,0)", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "FK_ingredients_recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "recipe",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_RecipeId",
                table: "ingredients",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingredients");
        }
    }
}
