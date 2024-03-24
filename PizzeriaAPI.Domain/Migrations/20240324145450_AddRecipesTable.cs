using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recipe",
                columns: table => new
                {
                    row_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recipe_name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_recipe_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ItemId",
                table: "recipe",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe");
        }
    }
}
