

using CocktailsApp.Domain.Cocktails;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Ingredients;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Cocktails
{
    public sealed class IngredientAddedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<IngredientAddedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;
        public async Task HandleAsync(IngredientAddedEvent @event, CancellationToken cancellationToken)
        {
            var ingredient = await _dbContext.Set<IngredientRead>()
                .FirstOrDefaultAsync(i => i.IngredientId == @event.IngredientId);

            _dbContext.Add(new CocktailIngredientRead()
            {
                CocktailId = @event.CocktailId,
                IngredientId = @event.CocktailId,
                CocktailIngredientId = @event.CocktailIngredientId,
                Quantity = @event.Quantity,
                Unit = @event.Unit.ToString(),
                IngredientName = ingredient?.Name!,
                IngredientType = ingredient?.IngredientType!,
                IsAlcoholic = ingredient?.IsAlcoholic ?? false,
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
