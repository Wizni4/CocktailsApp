using CocktailsApp.API.Common;


namespace CocktailsApp.API.Clubs
{
    public sealed record CreateRoleRequest(
        string Name,
        IEnumerable<string>? Permissions
    ) : IRequest;
}
