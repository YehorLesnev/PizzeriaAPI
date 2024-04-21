using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddGetProductOfMonthProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetProductOfMonth', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetProductOfMonth;
END

GO
CREATE PROCEDURE GetProductOfMonth
    @Year INT,
    @Month INT
AS
BEGIN
    SELECT TOP 1
		i.item_category AS Category,
        i.item_name AS ProductName,
        SUM(oi.quantity) AS QuantitySold
    FROM
        orders o
    INNER JOIN
        order_items oi ON o.order_id = oi.order_id
    INNER JOIN
        items i ON oi.item_id = i.item_id
    WHERE
        YEAR(o.date) = @Year
        AND MONTH(o.date) = @Month
    GROUP BY
        i.item_name, i.item_category
    ORDER BY
        SUM(oi.quantity) DESC;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetProductOfMonth', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetProductOfMonth;
END
");
        }
    }
}
