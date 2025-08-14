
using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record UpdateRoleRequest(
        Guid Id,
        string? Name,
        IEnumerable<string>? Permissions
    ) : IRequest;
}
