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
    public class CocktailByIdSpecification(Guid id) : ByIdSpecification<DomainCocktail>(id)
    {
    }
}
