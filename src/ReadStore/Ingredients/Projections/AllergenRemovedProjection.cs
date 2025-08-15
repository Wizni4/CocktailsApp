

using CocktailsApp.Domain.Ingredients;
using CocktailsApp.ReadStore.Persistence;
using CocktailsApp.ReadStore.Projections;

using Microsoft.EntityFrameworkCore;

namespace CocktailsApp.ReadStore.Ingredients
{
    public sealed class AllergenRemovedProjection(
        EFReadDbContext dbContext
    ) : IProjectionHandler<AllergenRemovedEvent>
    {
        private readonly EFReadDbContext _dbContext = dbContext;

        public async Task HandleAsync(AllergenRemovedEvent @event, CancellationToken cancellationToken)
        {
            var allergen = await _dbContext.Set<AllergenRead>()
                .FirstOrDefaultAsync(a =>
                     a.IngredientId == @event.IngredientId &&
                     a.Name == @event.Name);
            if (allergen != null) _dbContext.Remove(allergen);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
