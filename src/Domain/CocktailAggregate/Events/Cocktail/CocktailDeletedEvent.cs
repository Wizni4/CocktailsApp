/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public sealed record CocktailDeletedEvent(
        Guid CocktailId,
        Guid ActorId
    ) : DomainEvent(CocktailId, typeof(Cocktail), ActorId);
}
