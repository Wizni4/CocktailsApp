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
    public class SignOutCommandValidator : AbstractValidator<SignOutCommand>
    {
        public SignOutCommandValidator()
        {
            RuleFor(c => c.Username).ValidString();
        }
    }
}
