using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RenameStaffPhoneNumberCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "staff",
                newName: "phone_number");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "customers",
                type: "varchar(25)",
                fixedLength: true,
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldFixedLength: true,
                oldMaxLength: 15,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "staff",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "customers",
                type: "varchar(15)",
                fixedLength: true,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldFixedLength: true,
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}
