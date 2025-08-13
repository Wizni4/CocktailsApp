/*
 * Framework namespaces
 */
/*
 * Application namespaces
 */
using AutoMapper;

using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class GetClubByIdQueryHandler(
        IClubReader clubReader,
        IMapper autoMapper
    ) : IQueryHandler<GetClubByIdQuery, ClubDTO?>
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
