/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Cocktails
{
    public class CocktailByIdSpecification(Guid id) : ByIdSpecification<Cocktail>(id)
    {
    }
}
