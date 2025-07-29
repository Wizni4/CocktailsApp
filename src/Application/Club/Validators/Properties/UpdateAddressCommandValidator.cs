/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="UpdateAddressCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAddressCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateAddressCommand"/>.
        /// </summary>
        public UpdateAddressCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.NewAddress)
                .NotNull()
                .WithMessage("NewAddress must be provided.")
                .ChildRules(a =>
                {
                    a.RuleFor(a => a.City)
                         .NotNull()
                         .WithMessage("City must be provided.")
                         .NotEmpty()
                         .WithMessage("City cannot be empty.");

                    a.RuleFor(a => a.Country)
                        .NotNull()
                        .WithMessage("Country must be provided.")
                        .NotEmpty()
                        .WithMessage("Country cannot be empty.");

                    a.RuleFor(a => a.PostalCode)
                        .NotNull()
                        .WithMessage("Postal Code must be provided.")
                        .NotEmpty()
                        .WithMessage("Postal Code cannot be empty.");

                    a.RuleFor(a => a.State)
                        .NotNull()
                        .WithMessage("State must be provided.")
                        .NotEmpty()
                        .WithMessage("State cannot be empty.");

                    a.RuleFor(a => a.Street)
                        .NotNull()
                        .WithMessage("Street must be provided.")
                        .NotEmpty()
                        .WithMessage("Street cannot be empty.");

                    a.RuleFor(a => a.StreetNumber)
                        .NotNull()
                        .WithMessage("Street Number must be provided.")
                        .NotEmpty()
                        .WithMessage("Street Number cannot be empty.");
                });

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
