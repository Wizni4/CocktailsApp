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
        private readonly IClubReader _clubReader = clubReader;
        private readonly IMapper _autoMapper = autoMapper;
        public Task<ClubDTO?> Handle(GetClubByIdQuery request, CancellationToken cancellationToken)
        {
            return _clubReader.FirstOrDefaultAsync(
                new ClubByIdQuerySpecification(request.ClubId, _autoMapper),
                cancellationToken);
        }
    }
}
