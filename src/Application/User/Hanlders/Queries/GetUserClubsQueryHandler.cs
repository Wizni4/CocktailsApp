/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Club;

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;
/*
 * Domain namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.User
{
    public class GetUserClubsQueryHandler(
        IMapper autoMapper,
        IUnitOfWork unitOfWork
    ) : IQueryHandler<GetUserClubsQuery, IEnumerable<ClubDTO>>
    {
        private readonly IMapper _autoMapper = autoMapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IEnumerable<ClubDTO>> Handle(GetUserClubsQuery request, CancellationToken cancellationToken)
        {
            var club = await _unitOfWork.Set<DomainClub>().ReadRangeAsync(
                new ClubByUserIdSpecification(request.UserId),
                opt => opt.Include(c => c.Members));

            return _autoMapper.Map<IEnumerable<ClubDTO>>(club);
        }
    }
}
