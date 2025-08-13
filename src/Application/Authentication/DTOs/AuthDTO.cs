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
    public sealed class AuthDTO : EntityDTO
    {
        public string AccessToken { get; set; } = default!;
        public string? IdToken { get; set; } = null!;
        public string? RefreshToken { get; set; } = null!;
    }
}
