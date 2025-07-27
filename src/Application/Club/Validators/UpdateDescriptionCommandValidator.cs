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
    /// Validates the <see cref="UpdateDescriptionCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateDescriptionCommandValidator : AbstractValidator<UpdateDescriptionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDescriptionCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateDescriptionCommand"/>.
        /// </summary>
        public UpdateDescriptionCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.NewDescription)
                .NotNull()
                .WithMessage("NewDescription must be provided.")
                .NotEmpty()
                .WithMessage("NewDescription cannot be empty.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
