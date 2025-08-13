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

namespace CocktailsApp.Application.Club
{
    public class ClubService(
        IOptions<ClubSettingsDTO> clubSettings,
        IClubReader clubReader,
        IMapper autoMapper
    ) : IClubService
    {
        private readonly IOptions<ClubSettingsDTO> _clubSettings = clubSettings;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ClubLimitInfoDTO> GetClubLimitInfoAsync(Guid userId, CancellationToken cancellationToken)
        {
            // Get number of club owned by the user
            var ownedClubs = (await _unitOfWork.Set<DomainClub>().ReadRangeAsync(new ClubByOwnerIdSpecification(userId))).Count();

            // Get the max number of owned clubs
            var maxOwnedClubs = _clubSettings.Value.MaxOwnedClubs;

            return new ClubLimitInfoDTO()
            {
                MaxNumberOfOwnedClubs = maxOwnedClubs,
                NumberOfOwnedClubs = ownedClubs,
                CanCreateClub = ownedClubs < maxOwnedClubs
            };
        }
    }
}
