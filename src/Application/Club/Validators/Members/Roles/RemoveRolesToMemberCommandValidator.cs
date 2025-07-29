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
    /// Validates the <see cref="RemoveRolesToMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveRolesToMemberCommandValidator : AbstractValidator<RemoveRolesToMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRolesToMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveRolesToMemberCommand"/>.
        /// </summary>
        public RemoveRolesToMemberCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleIds)
                .NotNull()
                .WithMessage("RoleIds must not be null.")
                .NotEmpty()
                .WithMessage("RoleIds must not be an empty list.");

            RuleForEach(c => c.RoleIds)
               .NotEqual(Guid.Empty)
               .WithMessage("RoleId must be a valid enum value.");

            RuleFor(c => c.MemberId)
                .NotEqual(Guid.Empty)
               .WithMessage("MemberId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
