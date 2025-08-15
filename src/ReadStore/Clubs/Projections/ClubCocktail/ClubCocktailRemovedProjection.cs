
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubCocktailRemovedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubCocktailRemovedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubCocktailRemovedEvent @event, CancellationToken cancellationToken)
        {
            var clubCocktail = await _dbContext.Set<ClubCocktailRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId && c.ClubCocktailId == @event.ClubCocktailId, cancellationToken);
            if (clubCocktail != null) _dbContext.Remove(clubCocktail);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
