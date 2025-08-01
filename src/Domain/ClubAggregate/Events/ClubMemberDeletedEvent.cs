/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubMemberDeletedEvent : DomainEvent
    {
        public Guid ClubId { get; }
        public Guid ClubMemberId { get; }
        public ClubMemberDeletedEvent(Guid clubId, Guid clubMemberId)
        {
            ClubId = clubId;
            ClubMemberId = clubMemberId;
        }
    }
}
