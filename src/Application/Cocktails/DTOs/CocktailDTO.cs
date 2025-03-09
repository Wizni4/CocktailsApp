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

namespace CocktailsApp.Application.Cocktails
{
    public class CocktailDTO : EntityDTO
    {
        public required string Name { get; set; }
    }
}
