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
    /// Validates the <see cref="RemovePermissionToMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemovePermissionToMemberCommandValidator : AbstractValidator<RemovePermissionToMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePermissionToMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemovePermissionToMemberCommand"/>.
        /// </summary>
        public RemovePermissionToMemberCommandValidator()
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

            RuleFor(c => c.Permission)
               .IsInEnum()
               .WithMessage("Permission must be a valid enum value.");
        }
    }
}
