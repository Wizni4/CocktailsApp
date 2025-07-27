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
    /// Validates the <see cref="UpdateOwnerCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOwnerCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateOwnerCommand"/>.
        /// </summary>
        public UpdateOwnerCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.NewOwnerId)
                .NotEqual(Guid.Empty)
               .WithMessage("NewOwnerId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
