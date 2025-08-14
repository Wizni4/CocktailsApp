/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.StockAggregate
{
    public sealed record IngredientOutOfStock(
        Guid StockId,
        Guid IngredientId,
        Guid ActorId
    ) : DomainEvent(StockId, typeof(Stock), ActorId);
}
