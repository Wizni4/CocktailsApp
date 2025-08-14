using CocktailsApp.Application.Common;

using MediatR;

namespace CocktailsApp.Application.Auth
{
    public sealed record SignOutCommand : ICommand<Unit>;
}
