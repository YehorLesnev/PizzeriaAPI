using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RenameIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "items",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "customers",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "address",
                newName: "address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "customers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "address_id",
                table: "address",
                newName: "id");
        }
    }
}
