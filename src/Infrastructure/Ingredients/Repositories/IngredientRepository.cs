using CocktailsApp.Application.Ingredients;
using CocktailsApp.Domain.Ingredients;
using CocktailsApp.Infrastructure.Common;
using CocktailsApp.Infrastructure.Persistence;


namespace CocktailsApp.Infrastructure.Ingredients
{
    public class IngredientRepository(
        EFWriteDbContext context,
        IIncludesService<Ingredient> includesService
    ) : EFRepository<Ingredient>(context, includesService), IIngredientRepository
    {
    }
}
