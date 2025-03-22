/*
 * Domain namespaces
 */

/*
 * Framework namespaces
 */
using System.Net;

namespace CocktailsApp.Domain.UserAggregate
{
    public interface IAuthenticationService
    {
        Task<string> SignInAsync(string username, string password);
        Task<string> SignUpAsync(string username, string password);
    }
}
