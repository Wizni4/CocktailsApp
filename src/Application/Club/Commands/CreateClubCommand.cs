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


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Command to create a new club.
    /// </summary>
    /// <param name="address">The address of the club, provided as an <see cref="AddressDTO"/>.</param>
    /// <param name="description">A textual description of the club.</param>
    /// <param name="name">The name of the club.</param>
    /// <param name="ownerId">The identifier of the user who will be the owner of the club.</param>
    /// <param name="visibility">The visibility setting of the club (e.g., public or private).</param>
    public record CreateClubCommand(
        AddressDTO Address,
        string Description,
        string Name,
        Guid OwnerId,
        ClubVisibility Visibility
    ) : Command<Guid>(OwnerId);
}
