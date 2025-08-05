/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public class CocktailByIdsSpecification(List<Guid> ids) : ByIdsSpecification<DomainCocktail>(ids)
    {
    }
}
