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
    /// Validates the <see cref="RemovePermissionsToMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemovePermissionsToMemberCommandValidator : AbstractValidator<RemovePermissionsToMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePermissionsToMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemovePermissionsToMemberCommand"/>.
        /// </summary>
        public RemovePermissionsToMemberCommandValidator()
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

            RuleFor(c => c.Permissions)
                .NotNull()
                .WithMessage("Permissions must not be null.")
                .NotEmpty()
                .WithMessage("Permissions must not be an empty list.");

            RuleForEach(c => c.Permissions)
               .IsInEnum()
               .WithMessage("Permission must be a valid enum value.");
        }
    }
}
