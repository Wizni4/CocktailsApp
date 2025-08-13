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
    public class ClubCocktailDeletedEventHandler(
        ILogger<ClubDeletedEventHandler> logger
    ) : INotificationHandler<ClubCocktailRemovedEvent>
    {
        private readonly ILogger<ClubDeletedEventHandler> _logger = logger;
        public Task Handle(ClubCocktailRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club cocktail: '{notification.ClubCocktailId}': was removed from the club: '{notification.ClubId}'.");
            return Task.CompletedTask;
        }
    }
}
