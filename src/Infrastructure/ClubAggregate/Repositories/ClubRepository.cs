/*
 * Domain namespaces
 */
using CocktailsApp.Application.Club;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Infrastructure.SeedWork;
/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.ClubAggregate
{
    /// <summary>
    /// Represents the repository implementation for the <see cref="Club"/> aggregate.
    /// Inherits generic data access behavior from <see cref="EFCommandRepository{Club}"/> and implements domain-specific operations defined by <see cref="IClubRepository"/>.
    /// </summary>
    public class ClubRepository(EFDbContext dbContext) : EFCommandRepository<Club>(dbContext), IClubRepository
    {
    }
}
