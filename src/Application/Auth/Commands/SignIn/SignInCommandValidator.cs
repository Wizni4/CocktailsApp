using CocktailsApp.Application.Common;

using FluentValidation;


namespace CocktailsApp.Application.Auth
{
    public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(c => c.Username).ValidString();
            RuleFor(c => c.Password).ValidString();
        }
    }
}
