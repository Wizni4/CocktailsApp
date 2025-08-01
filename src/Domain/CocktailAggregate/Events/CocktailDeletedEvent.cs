/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.CocktailAggregate
{
    public class CocktailDeletedEvent : DomainEvent
    {
        public Guid CocktailId { get; }
        public CocktailDeletedEvent(Guid cocktailId)
        {
            CocktailId = cocktailId;
        }
    }
}
