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
    /// Command to create a specific club role, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the permission change is being made.</param>
    /// <param name="RoleName">The name of the role to be created.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record CreateRoleCommand(Guid ClubId, string RoleName, Guid ActorId) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the unique identifier of the club where the permission is being granted.
        /// </summary>
        public Guid ClubId { get; } = ClubId;

        /// <summary>
        /// Gets the name of the role to be created.
        /// </summary>
        public string RoleName { get; } = RoleName;

        /// <summary>
        /// Gets the identifier of the actor performing the permission assignment.
        /// May represent either a club member or system user depending on context.
        /// </summary>
        public Guid ActorId { get; } = ActorId;
    }
}
