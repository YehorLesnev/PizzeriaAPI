using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInventoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventory_ingredients",
                table: "ingredients");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.AddColumn<long>(
                name: "QuantityInStock",
                table: "ingredients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "ingredients");

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.ingredient_id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_inventory_ingredients",
                table: "ingredients",
                column: "ingredient_id",
                principalTable: "inventory",
                principalColumn: "ingredient_id");
        }
    }
}
