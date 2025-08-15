

using CocktailsApp.Domain.Ingredients;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

namespace CocktailsApp.ReadStore.Ingredients
{
    public sealed class IngredientCreatedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<IngredientCreatedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(IngredientCreatedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new IngredientRead()
            {
                IngredientId = @event.IngredientId,
                Name = @event.Name,
                IngredientType = @event.IngredientId.ToString(),
                IsAlcoholic = @event.IsAlcoholic
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
