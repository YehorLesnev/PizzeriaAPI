using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddGetSalesDistributionByCategoryAndDayProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetSalesDistributionByCategoryAndDay', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetSalesDistributionByCategoryAndDay;
END
GO

CREATE PROCEDURE GetSalesDistributionByCategoryAndDay
    @CategoryName NVARCHAR(100),
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT
       DATEFROMPARTS(YEAR(date), MONTH(date), DAY(date)) AS SalesDate,
        SUM(oi.quantity) AS Quantity,
        SUM(oi.quantity * i.item_price) AS TotalRevenue
    FROM
        orders o
    INNER JOIN
        order_items oi ON o.order_id = oi.order_id
    INNER JOIN
        items i ON oi.item_id = i.item_id
    WHERE
        i.item_category = @CategoryName
        AND o.date BETWEEN @StartDate AND @EndDate
    GROUP BY
        DATEFROMPARTS(YEAR(date), MONTH(date), DAY(date))
    ORDER BY
        SalesDate;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetSalesDistributionByCategoryAndDay', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetSalesDistributionByCategoryAndDay;
END");
        }
    }
}