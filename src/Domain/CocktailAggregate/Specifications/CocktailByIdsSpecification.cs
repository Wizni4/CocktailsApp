/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailByIdsSpecification(List<Guid> ids) : ByIdsSpecification<Cocktail>(ids)
    {
    }
}
