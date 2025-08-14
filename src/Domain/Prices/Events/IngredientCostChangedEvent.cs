/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Prices
{
    public sealed record IngredientCostChangedEvent(
        Guid IngredientId,
        decimal OldCost,
        decimal NewCost,
        Guid ActorId
    ) : DomainEvent(IngredientId, typeof(Pricing), ActorId);

}
