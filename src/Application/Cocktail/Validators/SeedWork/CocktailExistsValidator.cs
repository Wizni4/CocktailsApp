
/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */

/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;


namespace CocktailsApp.Application.Cocktail
{
    public class CocktailExistsValidator(IRepository<DomainCocktail> cocktailRepository) : AggregateExistsValidator<DomainCocktail>(cocktailRepository)
    {
    }
}
