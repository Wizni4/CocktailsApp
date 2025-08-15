

using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubVisibilityChangedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubVisibilityChangedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubVisibilityChangedEvent @event, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId);
            if (club != null) club.Visibility = @event.Visibility.ToString();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
