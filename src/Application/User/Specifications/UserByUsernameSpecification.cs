/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.User
{
    public class UserByUsernameSpecification(string login) : Specification<DomainUser>
    {
        private readonly string _login = login;
        public override Expression<Func<DomainUser, bool>> SpecExpression
        {
            get
            {
                return user => user.Username == _login;
            }
        }
    }
}
