/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;
using Microsoft.Extensions.Options;
/*
 * Application namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    public class ClubService(
        IOptions<ClubSettingsDTO> clubSettings,
        IClubQueries clubRepository
    ) : IClubService
    {
        private readonly IOptions<ClubSettingsDTO> _clubSettings = clubSettings;
        private readonly IClubQueries _clubRepository = clubRepository;

        public async Task<ClubLimitInfoDTO> GetClubLimitInfoAsync(Guid userId)
        {
            // Get number of club owned by the user
            var ownedClubs = (await _clubRepository.ReadRangeAsync(new ClubByOwnerIdSpecification(userId))).Count();

            // Get the max number of owned clubs
            var maxOwnedClubs = _clubSettings.Value.MaxOwnedClubs;

            return new ClubLimitInfoDTO(
                ownedClubs,
                maxOwnedClubs,
                ownedClubs < maxOwnedClubs);
        }
    }
}
