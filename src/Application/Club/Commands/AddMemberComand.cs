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
    /// Command to add a new member to a club, initiated by an existing actor (such as an admin or member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club to which the member is being added.</param>
    /// <param name="NewMemberUserId">The user ID of the individual to be added as a new club member.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddMemberCommand(Guid ClubId, Guid NewMemberUserId, Guid ActorId) : ICommand<ClubDTO>
    {
        /// <summary>
        /// Gets the unique identifier of the <see cref="Domain.ClubAggregate.Club"/> to which the <see cref="Domain.CocktailAggregate.Cocktail"/> will be added.
        /// </summary>
        public Guid ClubId { get; } = ClubId;

        /// <summary>
        /// Gets the <see cref="Domain.UserAggregate.User.Id"/> of the individual to be added as a new <see cref="Domain.ClubAggregate.ClubMember"/>.
        /// </summary>
        public Guid NewMemberUserId { get; } = NewMemberUserId;

        /// <summary>
        /// Gets the identifier of the actor performing the action.
        /// This typically refers to a <see cref="ClubMember"/>, but may also represent a <see cref="Domain.UserAggregate.User"/>.
        /// </summary>
        public Guid ActorId { get; } = ActorId;
    }

}
