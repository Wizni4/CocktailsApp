/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using Application.SeedWork;
/*
 * Framework namespaces
 */

namespace Application.Cocktails
{
    public class CocktailDTO : EntityDTO
    {
        public required string Name { get; set; }
    }
}
