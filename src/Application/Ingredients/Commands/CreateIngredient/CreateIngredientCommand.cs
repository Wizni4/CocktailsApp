using CocktailsApp.Application.Common;
using CocktailsApp.Domain.Ingredients;


namespace CocktailsApp.Application.Ingredients
{
    public sealed record CreateIngredientCommand(
        List<string>? Allergens,
        string Name,
        IngredientType Type,
        bool? IsAlcoholic,
        Guid RequestId
    ) : ICommand<Guid>, IIdempotentCommand
    {
        public string IdempotencyKey => $"CreateIngredient:{RequestId}";
    }
}
