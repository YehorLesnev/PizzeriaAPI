using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class DecreaseIngredientQuantityOnOrderItemInsertTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.DecreaseIngredientQuantityOnOrderItemInsert', 'TR') IS NOT NULL
BEGIN
	DROP TRIGGER dbo.DecreaseIngredientQuantityOnOrderItemInsert;
END;

GO
CREATE TRIGGER DecreaseIngredientQuantityOnOrderItemInsert
ON order_items
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OrderId UNIQUEIDENTIFIER;
    DECLARE @ItemId UNIQUEIDENTIFIER;
    DECLARE @Quantity INT;

    -- Cursor to iterate over each inserted order item
    DECLARE orderItemsCursor CURSOR FOR
    SELECT oi.order_id, oi.item_id, oi.quantity
    FROM inserted oi;

    OPEN orderItemsCursor;

    FETCH NEXT FROM orderItemsCursor INTO @OrderId, @ItemId, @Quantity;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        UPDATE ingredients
        SET quantity_in_stock = quantity_in_stock - (oi.quantity * ri.ingredient_weight)
		FROM ingredients 
		INNER JOIN recipe_ingredients ri ON ingredients.ingredient_id = ri.ingredient_id
		INNER JOIN recipes r ON ri.recipe_id = r.recipe_id
		INNER JOIN items i ON r.recipe_id = i.recipe_id
		INNER JOIN order_items oi ON i.item_id = oi.item_id
		WHERE i.item_id=@ItemId
		AND oi.order_id=@OrderId;

        FETCH NEXT FROM orderItemsCursor INTO @OrderId, @ItemId, @Quantity;
    END;

    CLOSE orderItemsCursor;
    DEALLOCATE orderItemsCursor;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.DecreaseIngredientQuantityOnOrderItemInsert', 'TR') IS NOT NULL
BEGIN
	DROP TRIGGER dbo.DecreaseIngredientQuantityOnOrderItemInsert;
END;");
        }
    }
}
