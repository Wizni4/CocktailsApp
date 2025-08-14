
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed record AddMembersCommand(
        Guid ClubId,
        IEnumerable<AddMemberModel> NewMembers
    ) : ClubCommand<IEnumerable<Guid>>(ClubId), IIdempotentCommand;

    public sealed record AddMemberModel(
        Guid UserId,
        IEnumerable<Guid>? RoleIds
    );
}
