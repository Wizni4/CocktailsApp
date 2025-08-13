using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;


namespace CocktailsApp.Domain.ClubAggregate
{
    public class ClubByTermSpecification(string term, Guid userId) : Specification<Club>
    {
        private readonly Guid _userId = userId;
        private readonly string _term = term;
        public override Expression<Func<Club, bool>> SpecExpression
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
