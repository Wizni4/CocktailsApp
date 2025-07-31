/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;

using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public interface ICocktailRepository : IRepository<DomainCocktail>
    {
    }
}
