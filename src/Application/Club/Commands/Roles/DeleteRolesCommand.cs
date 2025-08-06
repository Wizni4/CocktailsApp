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

using MediatR;


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Command to delete roles, initiated by another actor (typically an admin or authorized member).
    /// </summary>
    /// <param name="ClubId">The unique identifier of the club where the role is being deleted.</param>
    /// <param name="RoleIds">The list of unique identifiers of roles to delete.</param>
    /// <param name="ActorId">
    /// The identifier of the actor performing the action.
    /// This may be a <see cref="ClubMember.UserId"/> or a <see cref="ClubMember.Id"/>, depending on the context.
    /// The domain logic is responsible for resolving and authorizing the actor.
    /// </param>
    public record DeleteRolesCommand(
        Guid ClubId,
        IEnumerable<Guid> RoleIds,
        Guid ActorId
    ) : ClubCommand<Unit>(ClubId, ActorId);
}
