/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubDeletedEvent : DomainEvent
    {
        public Guid ClubId { get; }
        internal ClubDeletedEvent(Guid clubId)
        {
            ClubId = clubId;
        }
    }
}
