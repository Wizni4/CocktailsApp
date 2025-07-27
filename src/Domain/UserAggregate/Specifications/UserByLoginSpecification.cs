/*
 * Domain namespaces
 */
using CocktailsApp.Domain.SeedWork;

using System.Linq.Expressions;

/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.UserAggregate
{
    public class UserByLoginSpecification(string login) : Specification<User>
    {
        private readonly string _login = login;
        public override Expression<Func<User, bool>> SpecExpression
        {
            get
            {
                return user => user.Login == _login;
            }
        }
    }
}
