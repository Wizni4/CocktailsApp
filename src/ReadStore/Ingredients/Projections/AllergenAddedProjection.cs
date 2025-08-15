

using CocktailsApp.Domain.Ingredients;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

namespace CocktailsApp.ReadStore.Ingredients
{
    public sealed class AllergenAddedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<AllergenAddedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(AllergenAddedEvent @event, CancellationToken cancellationToken)
        {
            _dbContext.Add(new AllergenRead()
            {
                IngredientId = @event.IngredientId,
                Name = @event.Name
            });

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
