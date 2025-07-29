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
    public class SignInResponseDTO
    {
        public required string AccessToken { get; set; }
        public string? IdToken { get; set; }
        public string? RefreshToken { get; set; }
        public int? ExpiresIn { get; set; }
    }
}
