/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.IngredientPricingAggregate
{
    public sealed record IngredientCostChangedEvent(
        Guid IngredientId,
        decimal OldCost,
        decimal NewCost,
        Guid ActorId
    ) : DomainEvent(IngredientId, typeof(IngredientPricing), ActorId);

}
