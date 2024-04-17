using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteOrderAndUpdateInventoryTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.trg_DeleteOrderAndUpdateInventory', 'TR') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP TRIGGER dbo.trg_DeleteOrderAndUpdateInventory;
END

GO
CREATE TRIGGER trg_DeleteOrderAndUpdateInventory
ON order_items
FOR DELETE
AS
BEGIN
    DECLARE @OrderId UNIQUEIDENTIFIER;
    DECLARE @ItemId UNIQUEIDENTIFIER;
	DECLARE @Quantity INT;

    -- Iterate through the deleted rows
    DECLARE order_cursor CURSOR FOR
    SELECT order_id, item_id, quantity FROM deleted;

    OPEN order_cursor;
    FETCH NEXT FROM order_cursor INTO @OrderId, @ItemId, @Quantity;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Update inventory
	    UPDATE ingredients
        SET quantity_in_stock = quantity_in_stock + recipe_ingredients.ingredient_weight * @Quantity
        FROM items
        INNER JOIN recipe_ingredients ON items.recipe_id = recipe_ingredients.recipe_id
        INNER JOIN ingredients ON recipe_ingredients.ingredient_id = ingredients.ingredient_id
        WHERE items.item_id = @ItemId;

        FETCH NEXT FROM order_cursor INTO @OrderId, @ItemId, @Quantity;
    END;

    CLOSE order_cursor;
    DEALLOCATE order_cursor;
END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.trg_DeleteOrderAndUpdateInventory', 'TR') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP TRIGGER dbo.trg_DeleteOrderAndUpdateInventory;
END
");
        }
    }
}
