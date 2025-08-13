/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

using MediatR;

using Microsoft.Extensions.Logging;

/*
* Framework namespaces
*/

namespace CocktailsApp.Application.Club
{
    public class ClubMemberDeletedEventHandler(
        ILogger<ClubMemberDeletedEventHandler> logger
    ) : INotificationHandler<ClubMemberRemovedEvent>
    {
        private readonly ILogger<ClubMemberDeletedEventHandler> _logger = logger;

        public Task Handle(ClubMemberRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club member: '{notification.ClubMemberId}', was removed from the club: '{notification.ClubId}'.");
            // TODO: send email, publish event, audit, etc.
            return Task.CompletedTask;
        }
    }
}
