/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailDTO : EntityDTO
    {
        public required string Descritpion { get; set; }
        public required IEnumerable<CocktailIngredientDTO> Ingredients { get; set; }
        public required string Name { get; set; }
    }
}
