/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public record AddMembersCommand(
        Guid ClubId,
        IEnumerable<AddMemberModel> NewMembers,
        Guid ActorId
    ) : ClubCommand<IEnumerable<Guid>>(ClubId, ActorId);

    public class AddMemberModel
    {
        public required Guid UserId { get; set; }
        public IEnumerable<Guid>? RoleIds { get; set; }
    }
}
