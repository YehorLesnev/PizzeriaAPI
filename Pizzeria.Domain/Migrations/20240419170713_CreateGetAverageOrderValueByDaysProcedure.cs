﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateGetAverageOrderValueByDaysProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetAverageOrderValueByDays', 'P') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP PROCEDURE dbo.GetAverageOrderValueByDays;
END

GO
CREATE PROCEDURE GetAverageOrderValueByDays
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
        CONVERT(DATE, orders.date) AS Date,
        AVG(orders.order_total) AS AverageOrderTotal
    FROM orders
    GROUP BY CONVERT(DATE, orders.date);

    SELECT * FROM #AverageOrderValue ORDER BY Date;

    DROP TABLE #AverageOrderValue;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.GetAverageOrderValueByDays', 'P') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP PROCEDURE dbo.GetAverageOrderValueByDays;
END");
        }
    }
}