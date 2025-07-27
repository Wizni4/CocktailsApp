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
    /// Validates the <see cref="AddMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddMemberCommand"/> properties.
        /// </summary>
        public AddMemberCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.NewMemberUserId)
                .NotEqual(Guid.Empty)
                .WithMessage("NewMemberUserId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
