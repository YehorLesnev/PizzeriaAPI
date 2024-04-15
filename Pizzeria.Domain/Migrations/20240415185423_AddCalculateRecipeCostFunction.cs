using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculateRecipeCostFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.CalculateRecipeCost', 'FN') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP FUNCTION dbo.CalculateRecipeCost;
END

-- Create the function
GO
CREATE FUNCTION [dbo].[CalculateRecipeCost]
(
    @RecipeId UNIQUEIDENTIFIER
)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(18, 2);

    SELECT @TotalCost = 
    SUM(i.ingredient_price * ri.ingredient_weight)
    FROM recipe_ingredients ri
    INNER JOIN ingredients i ON ri.ingredient_id = i.ingredient_id
    WHERE ri.recipe_id = @RecipeId;

    RETURN ISNULL(@TotalCost, 0.00);
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF OBJECT_ID('dbo.CalculateRecipeCost', 'FN') IS NOT NULL
BEGIN
    -- Function exists, so alter it
    DROP FUNCTION dbo.CalculateRecipeCost;
END");
        }
    }
}
