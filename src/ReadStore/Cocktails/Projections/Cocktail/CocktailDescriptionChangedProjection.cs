

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class CocktailDescriptionChangedProjection(
        EFReadDbContext dbContext    
    ) : IProjectionHandler<CocktailDescriptionChangedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(CocktailDescriptionChangedEvent @event, CancellationToken cancellationToken)
        {
            var cocktail = await _dbContext.Set<CocktailRead>()
                .FirstOrDefaultAsync(c => c.CocktailId == @event.CocktailId, cancellationToken);
            if ( cocktail != null ) cocktail.Description = @event.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
