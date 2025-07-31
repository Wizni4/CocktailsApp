/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public record AddMembersCommand(
        Guid ClubId,
        IEnumerable<AddMemberModel> NewMembers,
        Guid ActorId
    ) : ICommand<IEnumerable<ClubMemberDTO>>;

    public class AddMemberModel
    {
        public required Guid UserId { get; set; }
        public IEnumerable<Guid>? RoleIds { get; set; }
    }
}
