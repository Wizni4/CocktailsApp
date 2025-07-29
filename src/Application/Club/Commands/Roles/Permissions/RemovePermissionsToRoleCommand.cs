/*
*Framework namespaces
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
    /// Command to remove permissions to a role of a club, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the role is being deleted.</param>
    /// <param name="RoleId">The unique identifier of the member to whom the permission is being removed.</param>
    /// <param name="Permissions">The permissions to remove from the role.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record RemovePermissionsToRoleCommand(
        Guid ClubId,
        Guid RoleId,
        IEnumerable<ClubPermissionType> Permissions,
        Guid ActorId
    ) : ICommand<ClubDTO>;
}
