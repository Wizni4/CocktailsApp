/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktails
{
    public class CocktailIngredientDTO : EntityDTO
    {
        public required IngredientDTO Ingredient { get; set; }
        public required decimal Quantity { get; set; }
    }
}
