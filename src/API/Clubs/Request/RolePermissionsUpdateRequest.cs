using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record RolePermissionsUpdateRequest(
        Guid Id,
        IEnumerable<string> Permissions
    ) : IRequest;
}
