using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateUpdateOrderTotalTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.UpdateOrderTotalTrigger', 'TR') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP TRIGGER dbo.UpdateOrderTotalTrigger;
END

GO
CREATE TRIGGER UpdateOrderTotalTrigger
ON order_items
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Iterate through the affected orders
    DECLARE @OrderId UNIQUEIDENTIFIER;

    -- Cursor to iterate over the affected orders
    DECLARE order_cursor CURSOR FOR
    SELECT DISTINCT order_id FROM inserted UNION SELECT DISTINCT order_id FROM deleted;

    OPEN order_cursor;
    FETCH NEXT FROM order_cursor INTO @OrderId;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Calculate total order amount
        UPDATE orders
        SET order_total = dbo.CalculateOrderTotal(@OrderId)
        WHERE order_id = @OrderId;

        FETCH NEXT FROM order_cursor INTO @OrderId;
    END;

    CLOSE order_cursor;
    DEALLOCATE order_cursor;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.UpdateOrderTotalTrigger', 'TR') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP TRIGGER dbo.UpdateOrderTotalTrigger;
END
");
        }
    }
}
