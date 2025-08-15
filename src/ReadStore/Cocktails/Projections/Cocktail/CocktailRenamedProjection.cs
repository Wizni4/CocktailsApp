
using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    internal class CocktailRenamedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<CocktailRenamedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(CocktailRenamedEvent @event, CancellationToken cancellationToken)
        {
            var cocktail = await _dbContext.Set<CocktailRead>()
                .FirstOrDefaultAsync(c => c.CocktailId == @event.CocktailId, cancellationToken);
            if (cocktail != null) cocktail.Name = @event.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
