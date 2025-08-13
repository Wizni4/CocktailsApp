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

using System.Collections.ObjectModel;

/*
 * Domain namespaces
 */
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;

namespace CocktailsApp.Application.User
{
    public class GetUserClubsQueryHandler(
        IClubReader clubReader,
        IMapper autoMapper
    ) : IQueryHandler<GetUserClubsQuery, IEnumerable<ClubDTO>>
    {
        private readonly IClubReader _clubReader = clubReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<IEnumerable<ClubDTO>> Handle(GetUserClubsQuery request, CancellationToken cancellationToken)
        {
            return (await _clubReader.ListAsync(
                new ClubByUserIdQuerySpecification(request.UserId, _autoMapper),
                cancellationToken));
        }
    }
}
