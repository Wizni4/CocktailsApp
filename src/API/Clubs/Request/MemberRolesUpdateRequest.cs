using CocktailsApp.API.Common;

namespace CocktailsApp.API.Clubs
{
    public sealed record MemberRolesUpdateRequest(
        Guid MemberId,
        IEnumerable<Guid> RoleIds
    ) : IRequest;
}
