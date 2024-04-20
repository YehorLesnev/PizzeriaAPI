using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddGetAverageOrderValueByMonthProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetAverageOrderValueByMonth', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetAverageOrderValueByMonth;
END
GO

CREATE PROCEDURE GetAverageOrderValueByMonth
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    CREATE TABLE #AverageOrderValue (
        Date DATE,
        AverageOrderTotal DECIMAL(10, 2)
    );

    INSERT INTO #AverageOrderValue (Date, AverageOrderTotal)
    SELECT
        DATEFROMPARTS(YEAR(orders.date), MONTH(orders.date), 1) AS Date,
        AVG(orders.order_total) AS AverageOrderTotal
    FROM orders
    WHERE orders.date BETWEEN @StartDate AND @EndDate
    GROUP BY DATEFROMPARTS(YEAR(orders.date), MONTH(orders.date), 1);

    SELECT * FROM #AverageOrderValue ORDER BY Date;

    DROP TABLE #AverageOrderValue;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetAverageOrderValueByMonth', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetAverageOrderValueByMonth;
END");
        }
    }
}