
using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record AddMemberRequest(
        Guid UserId,
        IEnumerable<Guid>? RoleIds
    ) : IRequest;
}
