using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStaffIdIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_staff_id",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_staff_id",
                table: "orders",
                column: "staff_id")
                .Annotation("SqlServer:Include", new[] { "order_id", "date", "customer_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_staff_id",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "IX_orders_staff_id",
                table: "orders",
                column: "staff_id");
        }
    }
}
