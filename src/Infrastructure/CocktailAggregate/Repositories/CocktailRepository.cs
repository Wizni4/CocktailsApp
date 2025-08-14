/*
 * Domain namespaces
 */
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Infrastructure.SeedWork;

/*
* Framework namespaces
*/
using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.Infrastructure.CocktailAggregate
{
    public class CocktailRepository(EFWriteDbContext dbContext) : EFRepository<Cocktail>(dbContext), ICocktailRepository
    {
    }
}
