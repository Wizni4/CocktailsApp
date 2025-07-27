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
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Club
{
    public interface IClubService : IService<ClubDTO>
    {
        Task<ClubDTO> CreateClubAsync(AddressDTO address, string description, string name, int visibility, Guid userId);
    }
}
