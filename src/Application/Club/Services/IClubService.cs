/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Application namespaces
 */

namespace CocktailsApp.Application.Club
{
    public interface IClubService : IService
    {
        Task<ClubLimitInfoDTO> GetClubLimitInfoAsync(Guid userId);
    }
}
