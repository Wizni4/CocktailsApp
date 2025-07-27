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
    /// Validates the <see cref="RemoveMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveMemberCommandValidator : AbstractValidator<RemoveMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveMemberCommand"/>.
        /// </summary>
        public RemoveMemberCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.MemberId)
                .NotEqual(Guid.Empty)
               .WithMessage("MemberId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
