using CocktailsApp.Application.Common;

using MediatR;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record DeleteIngredientCommand(
        Guid Id,
        Guid RequestId
    ) : ICommand<Unit>, IIdempotentCommand
    {
        public string IdempotencyKey => $"DeleteIngredient:{RequestId}";
    }
}
