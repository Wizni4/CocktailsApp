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
    /// Command to remove a permission of a role of a club, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the role is being deleted.</param>
    /// <param name="RoleId">The unique identifier of the member to whom the permission is being removed.</param>
    /// <param name="Permission">The permission to remove from the role.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record RemoveRolePermissionCommand(Guid ClubId, Guid RoleId, ClubPermission Permission, Guid ActorId) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the unique identifier of the club to delete.
        /// </summary>
        public Guid ClubId { get; } = ClubId;

        /// <summary>
        /// Gets the unique identifier of the role to whom the permission is being removed.
        /// </summary>
        public Guid RoleId { get; } = RoleId;

        /// <summary>
        /// Gets Permission to remove from the role.
        /// </summary>
        public ClubPermission Permission { get; } = Permission;

        /// <summary>
        /// Gets the identifier of the actor performing the deletion.
        /// May represent either a club member or system user depending on context.
        /// </summary>
        public Guid ActorId { get; } = ActorId;
    }
}
