using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Cocktails
{
    public sealed record DeleteCocktailCommand(
        Guid Id
    ) : ICommand<Unit>, IIdempotentCommand;
}
