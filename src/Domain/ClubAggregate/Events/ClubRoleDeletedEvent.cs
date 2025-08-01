/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubRoleDeletedEvent : DomainEvent
    {
        public Guid ClubId { get; }
        public Guid ClubRoleId { get; }
        public ClubRoleDeletedEvent(Guid clubId, Guid clubRoleId)
        {
            ClubId = clubId;
            ClubRoleId = clubRoleId;
        }
    }
}
