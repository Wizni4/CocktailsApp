

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class IngredientUnitChangedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<IngredientUnitChangedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(IngredientUnitChangedEvent @event, CancellationToken cancellationToken)
        {
            var ingredient = await _dbContext.Set<CocktailIngredientRead>()
                .FirstOrDefaultAsync(i => i.CocktailIngredientId == @event.CocktailIngredientId, cancellationToken);
            if (ingredient != null) ingredient.Unit = @event.Unit.ToString();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
