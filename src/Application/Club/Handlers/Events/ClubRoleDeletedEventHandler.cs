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
    public class ClubRoleDeletedEventHandler(
        ILogger<ClubRoleDeletedEventHandler> logger
    ) : INotificationHandler<ClubRoleDeletedEvent>
    {
        private readonly ILogger<ClubRoleDeletedEventHandler> _logger = logger;

        public Task Handle(ClubRoleDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club member: '{notification.RoleId}', was removed from the club: '{notification.ClubId}'.");
            return Task.CompletedTask;
        }
    }
}
