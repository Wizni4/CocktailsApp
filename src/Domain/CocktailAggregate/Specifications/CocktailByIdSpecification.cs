/*
 * Domain namespaces
 */
using Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace Domain.CocktailAggregate
{
    public class CocktailByIdSpecification(Guid id) : ByIdSpecification<Cocktail>(id)
    {
    }
}
