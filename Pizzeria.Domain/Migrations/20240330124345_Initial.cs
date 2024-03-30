using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address1 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    address2 = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    zipcode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(55)", fixedLength: true, maxLength: 55, nullable: true),
                    last_name = table.Column<string>(type: "varchar(55)", fixedLength: true, maxLength: 55, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(15)", fixedLength: true, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                });

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

            migrationBuilder.CreateTable(
                name: "recipes",
                columns: table => new
                {
                    recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    recipe_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    cooking_time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipes", x => x.recipe_id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    last_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    position = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    hourly_rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ingredient_weight_measure = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ingredient_price = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "FK_ingredients_inventory",
                        column: x => x.ingredient_id,
                        principalTable: "inventory",
                        principalColumn: "ingredient_id");
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    item_category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    item_size = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: false),
                    item_price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.item_id);
                    table.ForeignKey(
                        name: "FK_items_recipes",
                        column: x => x.item_id,
                        principalTable: "recipes",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    delivery = table.Column<bool>(type: "bit", nullable: false),
                    delivery_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_address",
                        column: x => x.delivery_address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "FK_orders_customers",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_orders_staff",
                        column: x => x.staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_ingredients",
                columns: table => new
                {
                    recipe_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ingredient_weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_ingredients", x => new { x.recipe_id, x.ingredient_id });
                    table.ForeignKey(
                        name: "FK_recipe_ingredients_ingredients",
                        column: x => x.ingredient_id,
                        principalTable: "ingredients",
                        principalColumn: "ingredient_id");
                    table.ForeignKey(
                        name: "FK_recipe_ingredients_recipes",
                        column: x => x.recipe_id,
                        principalTable: "recipes",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => new { x.order_id, x.item_id });
                    table.ForeignKey(
                        name: "FK_order_items_items",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "item_id");
                    table.ForeignKey(
                        name: "FK_order_items_orders",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_item_id",
                table: "order_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_delivery_address_id",
                table: "orders",
                column: "delivery_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_staff_id",
                table: "orders",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_ingredients_ingredient_id",
                table: "recipe_ingredients",
                column: "ingredient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "recipe_ingredients");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "recipes");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "inventory");
        }
    }
}
