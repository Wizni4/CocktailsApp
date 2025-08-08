using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CocktailsApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "StockTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "IngredientPricing",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Ingredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CocktailIngredient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cocktail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ClubRole",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ClubMember",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ClubCocktail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Club",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "IngredientPricing");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CocktailIngredient");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cocktail");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ClubRole");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ClubMember");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ClubCocktail");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Club");
        }
    }
}
