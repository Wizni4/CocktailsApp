/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;
/*
 * Framework namespaces
 */

namespace CocktailsApp.Application.Authentication
{
    public class AuthDTO : EntityDTO
    {
        public required string AccessToken { get; set; }
        public string? IdToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
