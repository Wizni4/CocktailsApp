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
    /// Validator for the <see cref="CreateClubCommand"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that all required fields for creating a club are properly populated and valid.
    /// </remarks>
    public class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateClubCommandValidator"/> class.
        /// Defines validation rules for the <see cref="CreateClubCommand"/>.
        /// </summary>
        public CreateClubCommandValidator()
        {
            RuleFor(c => c.Address)
                .NotNull()
                .WithMessage("Address must be provided.")
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


            RuleFor(c => c.Description)
                .NotNull()
                .WithMessage("Description must be provided.")
                .NotEmpty()
                .WithMessage("Description cannot be empty.");

            RuleFor(c => c.Name)
                .NotNull()
                .WithMessage("Name must be provided.")
                .NotEmpty()
                .WithMessage("Name cannot be empty.");

            RuleFor(c => c.OwnerId)
                .NotEqual(Guid.Empty)
                .WithMessage("OwnerId must be a valid non-empty GUID.");

            RuleFor(c => c.Visibility)
                .IsInEnum()
                .WithMessage("Visibility must be a valid enum value.");
        }
    }
}
