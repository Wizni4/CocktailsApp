/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Application.Shared;

/*
 * Domain namespaces
 */
using CocktailsApp.Domain.ClubAggregate;

using MediatR;


namespace CocktailsApp.Application.Club
{
    public record UpdateClubCommand(
        Guid ClubId,
        AddressDTO? Address,
        string? Name,
        string? Description,
        ClubVisibility? Visibility,
        Guid ActorId
    ) : ClubCommand<Unit>(ClubId, ActorId);
}
