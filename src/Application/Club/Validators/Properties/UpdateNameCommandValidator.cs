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
    /// Validates the <see cref="UpdateNameCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateNameCommandValidator : AbstractValidator<UpdateNameCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNameCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateNameCommand"/>.
        /// </summary>
        public UpdateNameCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.NewName)
                .NotNull()
                .WithMessage("NewName must be provided.")
                .NotEmpty()
                .WithMessage("NewName cannot be empty.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
