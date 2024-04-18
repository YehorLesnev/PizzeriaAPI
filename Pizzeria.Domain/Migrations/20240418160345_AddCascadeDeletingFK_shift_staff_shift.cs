using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeletingFK_shift_staff_shift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shift_staff_shift",
                table: "shift_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_shift_staff_shift",
                table: "shift_staff",
                column: "shift_id",
                principalTable: "shifts",
                principalColumn: "shift_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shift_staff_shift",
                table: "shift_staff");

            migrationBuilder.AddForeignKey(
                name: "FK_shift_staff_shift",
                table: "shift_staff",
                column: "shift_id",
                principalTable: "shifts",
                principalColumn: "shift_id");
        }
    }
}
