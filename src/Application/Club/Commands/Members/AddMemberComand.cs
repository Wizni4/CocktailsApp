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
    /// <param name="NewMemberUserId">The user ID to be added as new club members.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record AddMemberCommand(
        Guid ClubId,
        Guid NewMemberUserId,
        Guid ActorId
    ) : ICommand<ClubDTO>;
}
