/*
 * Framework namespaces
 */
using AutoMapper;

using CocktailsApp.Application.Clubs;

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.Clubs;

using System.Collections.ObjectModel;

/*
 * Domain namespaces
 */
using DomainClub = CocktailsApp.Domain.Clubs.Club;

namespace CocktailsApp.Application.Users
{
    public class GetUserClubsQueryHandler(
        IClubDetailsQueries clubReader,
        IMapper autoMapper
    ) : IQueryHandler<GetUserClubsQuery, IEnumerable<ClubDTO>>
    {
        private readonly IClubDetailsQueries _clubReader = clubReader;
        private readonly IMapper _autoMapper = autoMapper;
        public async Task<IEnumerable<ClubDTO>> Handle(GetUserClubsQuery request, CancellationToken cancellationToken)
        {
            return (await _clubReader.ListAsync(
                new ClubByUserIdQuerySpecification(request.UserId, _autoMapper),
                cancellationToken));
        }
    }
}
