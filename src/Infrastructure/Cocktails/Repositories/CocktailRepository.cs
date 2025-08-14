using CocktailsApp.Application.Cocktails;
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;



namespace CocktailsApp.Infrastructure.Cocktails
{
    public class CocktailRepository(
        EFWriteDbContext dbContext,
        IIncludesService<Cocktail> includesService
    ) : EFRepository<Cocktail>(dbContext, includesService), ICocktailRepository
    {
    }
}
