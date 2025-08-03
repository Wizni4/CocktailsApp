/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using MediatR;

using Microsoft.Extensions.Logging;

using DomainClubCocktail = CocktailsApp.Domain.ClubAggregate.ClubCocktail;
/*
* Framework namespaces
*/

namespace CocktailsApp.Application.Club
{
    public class ClubCocktailDeletedEventHandler(
        ILogger<ClubDeletedEventHandler> logger
    ) : INotificationHandler<ClubCocktailDeletedEvent>
    {
        private readonly ILogger<ClubDeletedEventHandler> _logger = logger;
        public Task Handle(ClubCocktailDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[DomainEvent] Club cocktail: '{notification.ClubCocktailId}': was removed from the club: '{notification.ClubId}'.");
            return Task.CompletedTask;
        }
    }
}
