
using CocktailsApp.Application.Cocktails;
using CocktailsApp.Domain.Clubs;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Clubs
{
    public sealed class ClubCocktailAddedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<ClubCocktailAddedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(ClubCocktailAddedEvent @event, CancellationToken cancellationToken)
        {
            var cocktail = await _dbContext.Set<CocktailListItem>()
                .FirstOrDefaultAsync(c => c.CocktailId == @event.CocktailId, cancellationToken);

            _dbContext.Add(new ClubCocktailRead()
            {
                ClubId = @event.ClubId,
                CocktailId = @event.CocktailId,
                ClubCocktailId = @event.ClubCocktailId,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
