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
    /// Validates the <see cref="DeleteClubCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class DeleteClubCommandValidator : AbstractValidator<DeleteClubCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteClubCommandValidator"/> class.
        /// Defines validation rules for the <see cref="DeleteClubCommand"/>.
        /// </summary>
        public DeleteClubCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
