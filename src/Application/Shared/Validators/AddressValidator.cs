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

namespace CocktailsApp.Application.Shared
{
    public class AddressValidator : AbstractValidator<AddressDTO>
    {
        public AddressValidator()
        {
            RuleFor(a => a)
                .NotNull()
                .WithMessage("Address must not be null.")
                .ChildRules(a =>
                {
                    RuleFor(a => a.City).ValidString();
                    RuleFor(a => a.Country).ValidString();
                    RuleFor(a => a.PostalCode).ValidString();
                    RuleFor(a => a.State).ValidString();
                    RuleFor(a => a.Street).ValidString();
                    RuleFor(a => a.StreetNumber).ValidString();
                });

        }
    }
}
