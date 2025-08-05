using CocktailsApp.Domain.SeedWork;
using System.Linq.Expressions;
using DomainClub = CocktailsApp.Domain.ClubAggregate.Club;
using CocktailsApp.Domain.ClubAggregate;

namespace CocktailsApp.Application.Search
{
    public class ClubByTermSpecification(string term, Guid userId) : Specification<DomainClub>
    {
        private readonly Guid _userId = userId;
        private readonly string _term = term;
        public override Expression<Func<DomainClub, bool>> SpecExpression 
        {
            get
            {
                return club => club.Name.Contains(_term) &&
                               (club.Visibility == ClubVisibility.Public ||
                                club.Members.Any(m => m.UserId == _userId));
            }
        } 
    }
}
