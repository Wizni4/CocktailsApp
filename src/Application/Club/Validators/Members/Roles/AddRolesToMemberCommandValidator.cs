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
    /// Validates the <see cref="AddRolesToMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddRolesToMemberCommandValidator : AbstractValidator<AddRolesToMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRolesToMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddRolesToMemberCommand"/> properties.
        /// </summary>
        public AddRolesToMemberCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.MemberId)
                .NotEqual(Guid.Empty)
                .WithMessage("MemberId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleIds)
                .NotNull()
                .WithMessage("RoleIds must not be null.")
                .NotEmpty()
                .WithMessage("RoleIds must not be an empty list.");

            RuleForEach(c => c.RoleIds)
                 .NotEqual(Guid.Empty)
                 .WithMessage("RoleIds must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
