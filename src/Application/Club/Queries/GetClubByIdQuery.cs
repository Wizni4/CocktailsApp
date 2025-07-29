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
using CocktailsApp.Domain.ClubAggregate;
using CocktailsApp.Domain.SeedWork;

namespace CocktailsApp.Application.Club
{
    public class GetClubByIdQuery(Guid clubId) : IQuery<Domain.ClubAggregate.Club, ClubDTO>
    {
        private readonly Guid _clubId = clubId;
        public ISpecification<Domain.ClubAggregate.Club> Specification => new ClubByIdSpecification(_clubId);

        public Func<IIncludable<Domain.ClubAggregate.Club>, IIncludable>? Include
        {
            get
            {
                return c => c.Include(c => c.Cocktails)
                             .Include(c => c.Owner)
                             .Include(c => c.Members)
                                .ThenInclude(m => m.Roles)
                             .Include(c => c.Roles);
            }
        }
    }
}
