/*
*Framework namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

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
    /// <param name="NewOwnerId">The unique identifier of the new owner of the club.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record UpdateOwnerCommand(Guid ClubId, Guid NewOwnerId, Guid ActorId) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the unique identifier of the club to delete.
        /// </summary>
        public Guid ClubId { get; } = ClubId;

        /// <summary>
        /// The unique identifier of the new owner of the club.
        /// </summary>
        public Guid NewOwnerId { get; } = NewOwnerId;

        /// <summary>
        /// Gets the identifier of the actor performing the deletion.
        /// May represent either a club member or system user depending on context.
        /// </summary>
        public Guid ActorId { get; } = ActorId;
    }
}
