using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddGetAverageOrderValueByYearProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetAverageOrderValueByYear', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetAverageOrderValueByYear;
END
GO

CREATE PROCEDURE GetAverageOrderValueByYear
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
        DATEFROMPARTS(YEAR(orders.date), 1, 1) AS Date,
        AVG(orders.order_total) AS AverageOrderTotal
    FROM orders
    WHERE orders.date BETWEEN @StartDate AND @EndDate
    GROUP BY YEAR(orders.date);

    SELECT * FROM #AverageOrderValue ORDER BY Date;

    DROP TABLE #AverageOrderValue;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetAverageOrderValueByYear', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.GetAverageOrderValueByYear;
END");
        }
    }
}