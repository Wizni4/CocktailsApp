using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;


namespace CocktailsApp.Application.Cocktails
{
    public sealed record CreateCocktailCommand(
        string Name,
        string? Description,
        List<CocktailIngredientModel> Ingredients
   ) : ICommand<Guid>, IIdempotentCommand;

    public sealed record CocktailIngredientModel(
        Guid Id,
        decimal Quantity,
        UnitOfMeasure Unit
    );
}
