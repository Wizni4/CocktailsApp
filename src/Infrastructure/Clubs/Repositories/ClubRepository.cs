
using CocktailsApp.Application.Clubs;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;


namespace CocktailsApp.Infrastructure.Clubs
{
    /// <summary>
    /// Represents the repository implementation for the <see cref="Club"/> aggregate.
    /// Inherits generic data access behavior from <see cref="EFRepository{Club}"/> and implements domain-specific operations defined by <see cref="IClubRepository"/>.
    /// </summary>
    public class ClubRepository(
        EFWriteDbContext dbContext,
        IIncludesService<Club> includesService
    ) : EFRepository<Club>(dbContext, includesService), IClubRepository;
}
