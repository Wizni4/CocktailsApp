/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;
using CocktailsApp.Application.User;
using CocktailsApp.Domain.ClubAggregate;

using MediatR;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class ClubService(IMediator mediator) : IClubService
    {
        private readonly IMediator _mediator = mediator;
        public Task<ClubDTO> CreateClubAsync(AddressDTO address, string description, string name, int visibility, Guid userId)
        {
            var command = new CreateClubCommand(
                address,
                description,
                name,
                userId,
                (ClubVisibility)visibility);
            return mediator.Send(command); // ✅ Validation + Handler + Mapping pipeline
        }
    }
}
