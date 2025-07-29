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
    /// Command to grant a permissions to a club member, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the permission change is being made.</param>
    /// <param name="MemberId">The unique identifier of the club member to whom the permission will be granted.</param>
    /// <param name="Permissions">The permissions to be granted to the member.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddPermissionsToMemberCommand(
        Guid ClubId,
        Guid MemberId,
        IEnumerable<ClubPermissionType> Permissions,
        Guid ActorId
    ) : ICommand<ClubDTO>;
}
