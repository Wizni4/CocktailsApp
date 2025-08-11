/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailByIdSpecification(Guid id) : ByIdSpecification<Cocktail>(id)
    {
    }
}
