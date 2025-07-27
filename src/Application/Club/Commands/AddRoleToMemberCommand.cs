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
    /// Command to grant a specific role to a club member, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the permission change is being made.</param>
    /// <param name="MemberId">The unique identifier of the club member to whom the role will be granted.</param>
    /// <param name="RoleId">The unique identifier of the role to be granted to the member.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddRoleToMemberCommand(Guid ClubId, Guid MemberId, Guid RoleId, Guid ActorId) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the unique identifier of the club where the permission is being granted.
        /// </summary>
        public Guid ClubId { get; } = ClubId;

        /// <summary>
        /// Gets the unique identifier of the club member to whom the permission will be added.
        /// </summary>
        public Guid MemberId { get; } = MemberId;

        /// <summary>
        /// Gets the unique identifier of the club role to be granted to the member.
        /// </summary>
        public Guid RoleId { get; } = RoleId;

        /// <summary>
        /// Gets the identifier of the actor performing the permission assignment.
        /// May represent either a club member or system user depending on context.
        /// </summary>
        public Guid ActorId { get; } = ActorId;
    }
}
