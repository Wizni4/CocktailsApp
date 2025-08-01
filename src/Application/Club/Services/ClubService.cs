/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using Microsoft.Extensions.Options;
/*
 * Application namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    public class ClubService(
        IMapper automapper,
        IOptions<ClubSettingsDTO> clubSettings,
        IUnitOfWork unitOfWork
    ) : IClubService
    {
        private readonly IMapper _automapper = automapper;
        private readonly IOptions<ClubSettingsDTO> _clubSettings = clubSettings;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ClubLimitInfo> GetClubLimitInfoAsync(Guid userId)
        {
            // Get number of club owned by the user
            var ownedClubs = (await _unitOfWork.Set<DomainClub>().ReadRangeAsync(new ClubByOwnerIdSpecification(userId))).Count();

            // Get the max number of owned clubs
            var maxOwnedClubs = _clubSettings.Value.MaxOwnedClubs;

            return new ClubLimitInfo(
                ownedClubs,
                maxOwnedClubs,
                ownedClubs < maxOwnedClubs);
        }
    }
}
