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
    /// Inherits generic data access behavior from <see cref="EFRepository{Club}"/> and implements domain-specific operations defined by <see cref="IClubRepository"/>.
    /// </summary>
    public class ClubRepository(EFDbContext dbContext) : EFRepository<Club>(dbContext), IClubRepository
    {
        public Task<Club?> GetClubBydIdAsync(Guid clubId, Func<IIncludable<Club>, IIncludable>? additionalIncludes = null)
        {
            // Defines the base include to manage permissions
            // Add add additional ones if specified
            Func<IIncludable<Club>, IIncludable> includes = c =>
            {
                var query = c.Include(club => club.Members)
                                .ThenInclude(member => member.Roles)
                             .Include(club => club.Roles);

                return additionalIncludes != null ? additionalIncludes(query) : query;
            };

            return base.ReadAsync(new ClubByIdSpecification(clubId), includes);
        }
    }
}
