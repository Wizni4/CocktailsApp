/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.SeedWork;
using Application.Shared;
/*
 * Framework namespaces
 */

namespace Application.Cocktails
{
    public class CocktailIngredientDTO : EntityDTO
    {
        public required IngredientDTO Ingredient { get; set; }
        public required decimal Quantity { get; set; }
    }
}
