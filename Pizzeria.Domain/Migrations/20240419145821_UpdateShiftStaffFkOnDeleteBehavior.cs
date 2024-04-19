using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShiftStaffFkOnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shift_staff_staff",
                table: "shift_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_shift_staff_staff",
                table: "shift_staff",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "staff_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shift_staff_staff",
                table: "shift_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_shift_staff_staff",
                table: "shift_staff",
                column: "staff_id",
                principalTable: "staff",
                principalColumn: "staff_id");
        }
    }
}
