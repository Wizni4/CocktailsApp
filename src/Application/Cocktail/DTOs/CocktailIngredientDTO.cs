/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public sealed class CocktailIngredientDTO : EntityDTO
    {
        public Guid Id { get; set; } = default!;
        public IngredientDTO Ingredient { get; set; } = default!;
        public decimal Quantity { get; set; } = default;
        public string Unit { get; set; } = default!;
    }
}
