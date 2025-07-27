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
    /// Validates the <see cref="UpdateVisibilityCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateVisibilityCommandValidator : AbstractValidator<UpdateVisibilityCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVisibilityCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateVisibilityCommand"/>.
        /// </summary>
        public UpdateVisibilityCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.Visibility)
                .IsInEnum()
                .WithMessage("Visibility must be a valid enum value.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
