/*
 * Domain namespaces
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
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(c => c.Username).ValidString();
            RuleFor(c => c.Password).ValidString();
        }
    }
}
