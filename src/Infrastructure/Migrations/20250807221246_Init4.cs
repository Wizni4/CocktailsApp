using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailsApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "User",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "StockTransaction",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Stock",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Order",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "IngredientPricing",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Ingredient",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "CocktailIngredient",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Cocktail",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ClubRole",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ClubMember",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ClubCocktail",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Club",
                newName: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "User",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "StockTransaction",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Stock",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Order",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "IngredientPricing",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Ingredient",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "CocktailIngredient",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Cocktail",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ClubRole",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ClubMember",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "ClubCocktail",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Club",
                newName: "ImageUrl");
        }
    }
}
