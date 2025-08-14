using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Cocktails
{
    public sealed record DeleteCocktailCommand(
        Guid Id,
        Guid RequestId
    ) : ICommand<Unit>, IIdempotentCommand
    {
        public string IdempotencyKey => $"DeleteCocktail:{RequestId}";
    }
}
