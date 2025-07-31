/*
*Domain namespaces
 */

/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;

using FluentValidation;


namespace CocktailsApp.Application.Authentication
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(c => c.RefreshToken).ValidString();
        }
    }
}
