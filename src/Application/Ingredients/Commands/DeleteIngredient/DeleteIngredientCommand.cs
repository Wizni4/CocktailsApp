using CocktailsApp.Application.Common;

using MediatR;

namespace CocktailsApp.Application.Ingredients
{
    public sealed record DeleteIngredientCommand(
        Guid Id
    ) : ICommand<Unit>, IIdempotentCommand;
}
