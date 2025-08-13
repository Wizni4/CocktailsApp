/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;

using System.Collections.ObjectModel;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Stock
{
    public sealed class StockDTO: EntityDTO
    {
        public Guid Id { get; set; } = default;
        public Guid ClubId { get; set; } = default;
        public IngredientDTO Ingredient { get; set; } = default!;
        public decimal Quantity { get; set; } = default;
        public string Unit { get; set; } = default!;
        public List<StockTransactionDTO> Transactions { get; set; } = default!;
    }
}
