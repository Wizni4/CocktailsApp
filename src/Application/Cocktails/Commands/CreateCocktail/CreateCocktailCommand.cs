using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Common;


namespace CocktailsApp.Application.Cocktails
{
    public sealed record CreateCocktailCommand(
        string? Description,
        List<IngredientModel> Ingredients,
        string Name,
        Guid RequestId
   ) : ICommand<Guid>, IIdempotentCommand
    {
        public string IdempotencyKey => $"CreateCocktail:{RequestId}";
    }

    public sealed record IngredientModel(
        Guid Id,
        decimal Quantity,
        UnitOfMeasure Unit
    );
}
