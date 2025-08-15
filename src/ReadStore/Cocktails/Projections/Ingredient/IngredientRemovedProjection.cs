

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class IngredientRemovedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<IngredientRemovedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(IngredientRemovedEvent @event, CancellationToken cancellationToken)
        {
            var ingredient = await _dbContext.Set<CocktailIngredientRead>()
                .FirstOrDefaultAsync(i => i.CocktailIngredientId == @event.CocktailIngredientId, cancellationToken);
            if (ingredient != null) _dbContext.Remove(ingredient);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
