

using CocktailsApp.Application.Clubs;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubRenamedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<ClubRenamedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(ClubRenamedEvent @event, CancellationToken cancellationToken)
        {
            var club = await _dbContext.Set<ClubRead>()
                .FirstOrDefaultAsync(c => c.ClubId == @event.ClubId);
            if (club != null) club.Name = @event.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
