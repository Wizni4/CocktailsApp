using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Auth
{
    public sealed record RefreshTokenCommand : ICommand<AuthTokens>;
}
