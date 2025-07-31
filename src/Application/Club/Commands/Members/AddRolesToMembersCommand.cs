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
using CocktailsApp.Domain.ClubAggregate;


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Command to grant roles to a club member, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the permission change is being made.</param>
    /// <param name="MemberId">The unique identifier of the club member to whom the role will be granted.</param>
    /// <param name="RoleId">The list of unique identifier of roles to be granted to the member.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddRolesToMembersCommand(
        Guid ClubId,
        IEnumerable<MemberRolesUpdateModel> Members,
        Guid ActorId
    ) : ICommand<IEnumerable<ClubMemberDTO>>;
}
