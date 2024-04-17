using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTotalColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "order_total",
                table: "orders",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_total",
                table: "orders");
        }
    }
}
