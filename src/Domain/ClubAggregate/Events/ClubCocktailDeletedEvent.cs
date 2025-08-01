/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubCocktailDeletedEvent : DomainEvent
    {
        public Guid ClubId { get; }
        public Guid ClubCocktailId { get; }
        public ClubCocktailDeletedEvent(Guid clubId, Guid clubCocktailId)
        {
            ClubId = clubId;
            ClubCocktailId = clubCocktailId;
        }
    }
}
