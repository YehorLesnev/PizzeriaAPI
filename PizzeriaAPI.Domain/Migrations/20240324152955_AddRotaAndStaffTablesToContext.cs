using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaAPI.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRotaAndStaffTablesToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "varchar(55)", maxLength: 55, nullable: false),
                    last_name = table.Column<string>(type: "varchar(55)", maxLength: 55, nullable: false),
                    position = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    hourly_rate = table.Column<decimal>(type: "Decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "rota",
                columns: table => new
                {
                    row_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rota_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "DateTime", nullable: false),
                    shift_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rota", x => x.row_id);
                    table.ForeignKey(
                        name: "FK_rota_staff_staff_id",
                        column: x => x.staff_id,
                        principalTable: "staff",
                        principalColumn: "staff_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rota_staff_id",
                table: "rota",
                column: "staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rota");

            migrationBuilder.DropTable(
                name: "staff");
        }
    }
}
