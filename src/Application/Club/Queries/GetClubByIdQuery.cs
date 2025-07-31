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
    public class GetClubByIdQuery(Guid clubId) : IQuery<Domain.ClubAggregate.Club, ClubDTO>
    {
        private readonly Guid _clubId = clubId;
        public ISpecification<DomainClub> Specification => new ClubByIdSpecification(_clubId);

        public Func<IIncludable<DomainClub>, IIncludable>? Include
        {
            get
            {
                return c => c.Include(c => c.Cocktails)
                             .Include(c => c.Members)
                                .ThenInclude(m => m.Roles)
                             .Include(c => c.Roles);
            }
        }
    }
}
