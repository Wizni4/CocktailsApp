
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;


namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubDescriptionChangedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubDescriptionChangedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubDescriptionChangedEvent @event, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId, cancellationToken);
            if (club != null) club.Description = @event.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
