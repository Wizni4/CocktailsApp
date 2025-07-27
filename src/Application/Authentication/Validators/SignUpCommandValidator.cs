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
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(c => c.Login)
                .NotNull()
                .WithMessage("Login must be provided.")
                .NotEmpty()
                .WithMessage("Login cannot be empty.")
                .Matches(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                .WithMessage("Email has an incorrect format. E.g: youreamiladress@domain.com");

            RuleFor(c => c.Password)
                .NotNull()
                .WithMessage("Password must be provided.")
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .Matches("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$")
                .WithMessage("Password must be at least 8 characters and contain at 3 of 4 of the following: upper case (A - Z), lower case (a - z), number(0 - 9) and special character(e.g. !@#$%^&*)");
        }
    }
}
