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
        public required string Name { get; set; }
    }
}
