

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Context;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class IngredientQuantityChangedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<IngredientQuantityChangedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(IngredientQuantityChangedEvent @event, CancellationToken cancellationToken)
        {
            var ingredient = await _dbContext.Set<CocktailIngredientRead>()
                .FirstOrDefaultAsync(i => i.CocktailIngredientId == @event.CocktailIngredientId, cancellationToken);
            if ( ingredient != null ) ingredient.Quantity = @event.Quantity;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
