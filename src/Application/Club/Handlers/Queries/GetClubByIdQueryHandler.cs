/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;
/*
 * Domain namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.Club
{
    public class GetClubByIdQueryHandler(IUnitOfWork unitOfWork, IMapper autoMapper)
        : IQueryHandler<GetClubByIdQuery, ClubDTO>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _autoMapper = autoMapper;

        public async Task<ClubDTO> Handle(GetClubByIdQuery request, CancellationToken cancellationToken)
        {
            var club = await _unitOfWork.Set<DomainClub>().ReadAsync(
                new ClubByIdSpecification(request.ClubId));
            return _autoMapper.Map<ClubDTO>(club);
        }
    }
}
