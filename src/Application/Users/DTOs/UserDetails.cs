

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Users
{
    public sealed record UserDetails(
        Guid Id,
        string Username,
        string? ImageId
    ) : IDTO;
}
