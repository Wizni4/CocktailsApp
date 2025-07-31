/*
 * Domain namespaces
 */
/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
/*
 * Application namespaces
 */
using DomainAddress = CocktailsApp.Domain.Shared.Address;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    public class UpdateClubCommandHanlder(
        IUnitOfWork unitOfWork,
        IClubRepository clubRepository,
        IMapper autoMapper
    ) : ICommandHandler<UpdateClubCommand, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IClubRepository _clubRepository = clubRepository;

        public async Task<ClubDTO> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            // Get the club from the db
            var club = await _clubRepository.GetClubBydIdAsync(request.ClubId);

            // Update Address
            if (request.Address != null) 
                club!.UpdateAddress(_autoMapper.Map<DomainAddress>(request.Address), request.ActorId);

            // Update Name
            if (request.Name != null)
                club!.UpdateName(request.Name, request.ActorId);

            // Update Description
            if (request.Description != null)
                club!.UpdateDescription(request.Description, request.ActorId);

            // Update Visibility
            if (request.Visibility != null)
                club!.UpdateVisibility(request.Visibility.Value, request.ActorId);

            // Persist the changes.
            _clubRepository.Update(club!);
            await _unitOfWork.SaveChangesAsync();

            // Return the updated club as a DTO.
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
