
using CocktailsApp.Application.Common;

using MediatR;


namespace CocktailsApp.Application.Auth
{
    public sealed record SignUpCommand(
        string Username,
        string Email,
        string Password
    ) : ICommand<Unit>;
}
