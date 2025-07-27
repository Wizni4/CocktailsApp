/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using MediatR;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class AddMemberComand(Guid clubId, Guid userId, Guid performingMemberId) : ICommand<ClubDTO>
    {
        public Guid ClubId { get; } = clubId;
        public Guid PerformingMemberId { get; } = performingMemberId;
        public Guid UserId { get; } = userId;
    }
}
