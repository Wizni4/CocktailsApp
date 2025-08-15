using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubDeletedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubDeletedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubDeletedEvent @event, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId, cancellationToken);
            if (club != null) _dbContext.Remove(club);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
