using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateCalculateStaffPayrollProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.CalculateStaffPayroll', 'P') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP PROCEDURE dbo.CalculateStaffPayroll;
END

GO
CREATE PROCEDURE CalculateStaffPayroll
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Create a temporary table to store staff payroll data
    CREATE TABLE #StaffPayroll (
        StaffId UNIQUEIDENTIFIER,
        FirstName VARCHAR(100),
        LastName VARCHAR(100),
		Position VARCHAR(100),
        HoursWorked DECIMAL(10, 2),
        HourlyRate DECIMAL(10, 2),
        Payroll DECIMAL(10, 2)
    );

    -- Insert data into the temporary table
    INSERT INTO #StaffPayroll (StaffId, FirstName, LastName, Position, HoursWorked, HourlyRate)
    SELECT s.staff_id, s.first_name, s.last_name, s.position,
           SUM(DATEDIFF(HOUR, sh.shift_start_time, sh.shift_end_time)) AS HoursWorked,
           s.hourly_rate
    FROM staff s
    INNER JOIN shift_staff ss ON s.staff_id = ss.staff_id
    INNER JOIN shifts sh ON ss.shift_id = sh.shift_id
    WHERE sh.shift_date BETWEEN @StartDate AND @EndDate
    GROUP BY s.staff_id, s.first_name, s.last_name, s.position, s.hourly_rate;

    -- Calculate payroll
    UPDATE #StaffPayroll
    SET Payroll = HoursWorked * HourlyRate;

    -- Select the calculated payroll data
    SELECT * FROM #StaffPayroll ORDER BY Payroll;

    -- Drop the temporary table
    DROP TABLE #StaffPayroll;
END;

EXEC CalculateStaffPayroll @StartDate = '2024-04-01', @EndDate = '2024-04-30';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.CalculateStaffPayroll', 'P') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP PROCEDURE dbo.CalculateStaffPayroll;
END");
        }
    }
}
