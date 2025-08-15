

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class CocktailDeletedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<CocktailDeletedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(CocktailDeletedEvent @event, CancellationToken cancellationToken)
        {
            var cocktail = await _dbContext.Set<CocktailRead>()
                .FirstOrDefaultAsync(c => c.CocktailId == @event.CocktailId, cancellationToken);
            if (cocktail != null) _dbContext.Remove(cocktail);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
