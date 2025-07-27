/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Infrastructure.SeedWork;

using MediatR;

using Microsoft.Extensions.Logging;
/*
* Framework namespaces
*/

namespace CocktailsApp.Infrastructure.ClubAggregate
{
    public class ClubDeletedEventHandler(ILogger<ClubDeletedEventHandler> logger) : INotificationHandler<ClubDeletedEvent>
    {
        private readonly ILogger<ClubDeletedEventHandler> _logger = logger;
        public Task Handle(ClubDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club {notification.ClubId} was deleted.");
            // TODO: send email, publish event, audit, etc.
            return Task.CompletedTask;
        }
    }
}
