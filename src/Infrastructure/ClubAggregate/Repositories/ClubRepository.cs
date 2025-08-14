/*
 * Domain namespaces
 */
using CocktailsApp.Application.Clubs;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Infrastructure.SeedWork;
/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the repository implementation for the <see cref="Club"/> aggregate.
    /// Inherits generic data access behavior from <see cref="EFRepository{Club}"/> and implements domain-specific operations defined by <see cref="IClubRepository"/>.
    /// </summary>
    public class ClubRepository(EFWriteDbContext dbContext) : EFRepository<Club>(dbContext), IClubRepository
    {
    }
}
