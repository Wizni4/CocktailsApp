
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed record AddMembersCommand(
        Guid ClubId,
        IEnumerable<AddMemberModel> NewMembers,
        Guid RequestId
    ) : ClubCommand<IEnumerable<Guid>>(ClubId), IIdempotentCommand
    {
        public string IdempotencyKey => $"AddMembers:{RequestId}";
    }

    public sealed record AddMemberModel(
        Guid UserId,
        IEnumerable<Guid>? RoleIds
    );
}
