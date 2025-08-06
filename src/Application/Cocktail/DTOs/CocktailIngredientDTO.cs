/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.Ingredient;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Shared;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailIngredientDTO : EntityDTO
    {
        public required IngredientDTO Ingredient { get; set; }
        public required decimal Quantity { get; set; }
        public required UnitOfMeasure Unit { get; set; }
    }
}
