/*
 * Domain namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Domain.CocktailAggregate;
using CocktailsApp.Infrastructure.SeedWork;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.Infrastructure.CocktailAggregate
{
    public class CocktailRepository(EFDbContext dbContext) : EFCommandRepository<Cocktail>(dbContext), ICocktailRepository
    {
    }
}
