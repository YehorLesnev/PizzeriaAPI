using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculateOrderTotalFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.CalculateOrderTotal', 'FN') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP FUNCTION dbo.CalculateOrderTotal;
END;

GO
CREATE FUNCTION CalculateOrderTotal
(
    @OrderId UNIQUEIDENTIFIER
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @Total DECIMAL(18, 2);

    SELECT @Total = SUM(items.item_price * order_items.quantity)
    FROM order_items
    INNER JOIN items ON order_items.item_id = items.item_id
    WHERE order_items.order_id = @OrderId;

    RETURN ISNULL(@Total, 0);
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.CalculateOrderTotal', 'FN') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP FUNCTION dbo.CalculateOrderTotal;
END;");
        }
    }
}