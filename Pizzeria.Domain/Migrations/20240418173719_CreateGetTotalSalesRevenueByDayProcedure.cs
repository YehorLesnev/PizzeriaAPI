using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateGetTotalSalesRevenueByDayProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetTotalSalesRevenueByDay', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetTotalSalesRevenueByDay;
END

GO
CREATE PROCEDURE GetTotalSalesRevenueByDay
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT
        CONVERT(DATE, o.date) AS SalesDate,
        SUM(oi.quantity * i.item_price) AS TotalRevenue
    FROM
        orders o
    INNER JOIN
        order_items oi ON o.order_id = oi.order_id
    INNER JOIN
        items i ON oi.item_id = i.item_id
    WHERE
        o.date BETWEEN @StartDate AND @EndDate
    GROUP BY
        CONVERT(DATE, o.date)
    ORDER BY
        SalesDate;
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetTotalSalesRevenueByDay', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetTotalSalesRevenueByDay;
END
");
        }
    }
}