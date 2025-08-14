/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    public sealed record ClubMemberRemovedEvent(
        Guid ClubId,
        Guid ClubMemberId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
