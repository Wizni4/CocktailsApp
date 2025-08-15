
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;



namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubImageUpdatedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubImageUpdatedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubImageUpdatedEvent @event, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId);
            if (club != null) club.ImageId = @event.ImageId;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
