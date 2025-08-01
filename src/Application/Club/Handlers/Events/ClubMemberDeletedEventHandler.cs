/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using DomainClubMember = CocktailsApp.Domain.ClubAggregate.ClubMember;
using MediatR;
using Microsoft.Extensions.Logging;
/*
* Framework namespaces
*/

namespace CocktailsApp.Application.Club
{
    public class ClubMemberDeletedEventHandler(
        ILogger<ClubMemberDeletedEventHandler> logger
    ) : INotificationHandler<ClubMemberDeletedEvent>
    {
        private readonly ILogger<ClubMemberDeletedEventHandler> _logger = logger;

        public Task Handle(ClubMemberDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club member: '{notification.ClubMemberId}', was removed from the club: '{notification.ClubId}'.");
            // TODO: send email, publish event, audit, etc.
            return Task.CompletedTask;
        }
    }
}
