/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Shared;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Cocktail
{
    public record CreateCocktailCommand(
        string? Description,
        List<IngredientModel> Ingredients,
        string Name,
        Guid CreatorId
   ) : ICommand<Guid>;

    public record IngredientModel(
        Guid Id,
        decimal Quantity,
        UnitOfMeasure Unit
    );
}
