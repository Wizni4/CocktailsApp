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
using FluentValidation;


namespace CocktailsApp.Application.Authentication
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(c => c.Login)
                .NotNull()
                .WithMessage("Login must be provided.")
                .NotEmpty()
                .WithMessage("Login cannot be empty.");

            RuleFor(c => c.Password)
                .NotNull()
                .WithMessage("Password must be provided.")
                .NotEmpty()
                .WithMessage("Password cannot be empty.");
        }
    }
}
