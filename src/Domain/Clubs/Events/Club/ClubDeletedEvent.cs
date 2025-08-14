/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Clubs
{
    public sealed record ClubDeletedEvent(
        Guid ClubId,
        Guid ActorId
    ) : DomainEvent(ClubId, typeof(Club), ActorId);
}
