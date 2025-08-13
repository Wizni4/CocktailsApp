/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public sealed record ClubMemberRemovedEvent(
        Guid ClubId,
        Guid ClubMemberId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
