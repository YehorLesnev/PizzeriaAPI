using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_customer_id",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_staff_first_name_last_name",
                table: "staff",
                columns: new[] { "first_name", "last_name" })
                .Annotation("SqlServer:Include", new[] { "staff_id", "position", "hourly_rate" });

            migrationBuilder.CreateIndex(
                name: "IX_shifts_shift_date",
                table: "shifts",
                column: "shift_date")
                .Annotation("SqlServer:Include", new[] { "shift_id", "shift_start_time", "shift_end_time" });

            migrationBuilder.CreateIndex(
                name: "IX_recipes_recipe_name",
                table: "recipes",
                column: "recipe_name")
                .Annotation("SqlServer:Include", new[] { "recipe_id", "cooking_time" });

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id")
                .Annotation("SqlServer:Include", new[] { "order_id", "date", "staff_id" });

            migrationBuilder.CreateIndex(
                name: "IX_items_item_category",
                table: "items",
                column: "item_category")
                .Annotation("SqlServer:Include", new[] { "item_id", "item_name", "item_size", "item_price", "image_path" });

            migrationBuilder.CreateIndex(
                name: "IX_items_item_name",
                table: "items",
                column: "item_name")
                .Annotation("SqlServer:Include", new[] { "item_id", "item_category", "item_size", "item_price", "image_path" });

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_ingredient_name",
                table: "ingredients",
                column: "ingredient_name")
                .Annotation("SqlServer:Include", new[] { "ingredient_id", "ingredient_price", "ingredient_weight_measure", "quantity_in_stock" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_first_name_last_name",
                table: "AspNetUsers",
                columns: new[] { "first_name", "last_name" })
                .Annotation("SqlServer:Include", new[] { "Id", "phone_number" });

            migrationBuilder.CreateIndex(
                name: "IX_address_city",
                table: "address",
                column: "city")
                .Annotation("SqlServer:Include", new[] { "address_id", "address1", "address2", "zipcode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_staff_first_name_last_name",
                table: "staff");

            migrationBuilder.DropIndex(
                name: "IX_shifts_shift_date",
                table: "shifts");

            migrationBuilder.DropIndex(
                name: "IX_recipes_recipe_name",
                table: "recipes");

            migrationBuilder.DropIndex(
                name: "IX_orders_customer_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_items_item_category",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_item_name",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_ingredients_ingredient_name",
                table: "ingredients");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_first_name_last_name",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_address_city",
                table: "address");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");
        }
    }
}
