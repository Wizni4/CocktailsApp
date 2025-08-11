/*
 * Framework namespaces
 */
using AutoMapper;
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Domain namespaces
 */

namespace CocktailsApp.Application.Club
{
    public class GetClubByIdQueryHandler(
        IClubQueries clubQueries,
        IMapper autoMapper
    ) : IQueryHandler<GetClubByIdQuery, ClubDTO?>
    {
        private readonly IClubQueries _clubQueries = clubQueries;

        public Task<ClubDTO?> Handle(GetClubByIdQuery request, CancellationToken cancellationToken)
        {
            return _clubQueries.ReadAsync(
                new ClubByIdQuerySpecification(request.ClubId));
        }
    }
}
