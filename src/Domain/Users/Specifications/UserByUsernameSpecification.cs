/*
 * Domain namespaces
 */
using CocktailsApp.Domain.Common;

using System.Linq.Expressions;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Domain.Users
{
    public class UserByUsernameSpecification(string login) : Specification<User>
    {
        private readonly string _login = login;
        public override Expression<Func<User, bool>> SpecExpression
        {
            get
            {
                return user => user.Username == _login;
            }
        }
    }
}
