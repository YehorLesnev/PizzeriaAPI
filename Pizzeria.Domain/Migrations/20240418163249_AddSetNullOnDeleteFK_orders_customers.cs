using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSetNullOnDeleteFK_orders_customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers",
                table: "orders");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers",
                table: "orders",
                column: "customer_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers",
                table: "orders");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers",
                table: "orders",
                column: "customer_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
