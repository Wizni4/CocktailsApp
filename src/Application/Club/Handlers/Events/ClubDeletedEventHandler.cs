/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using MediatR;
using Microsoft.Extensions.Logging;
/*
* Framework namespaces
*/

namespace CocktailsApp.Application.Club
{
    public class ClubDeletedEventHandler(
        ILogger<ClubDeletedEventHandler> logger
    ) : INotificationHandler<ClubDeletedEvent>
    {
        private readonly ILogger<ClubDeletedEventHandler> _logger = logger;

        public Task Handle(ClubDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club: '{notification.ClubId}', was deleted.");
            // TODO: send email, publish event, audit, etc.
            return Task.CompletedTask;
        }
    }
}
