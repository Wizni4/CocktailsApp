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
    /// Validates the <see cref="RemoveRoleToMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveRoleToMemberCommandValidator : AbstractValidator<RemoveRoleToMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRoleToMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveRoleToMemberCommand"/>.
        /// </summary>
        public RemoveRoleToMemberCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleId)
                .NotEqual(Guid.Empty)
               .WithMessage("RoleId must be a valid non-empty GUID.");

            RuleFor(c => c.MemberId)
                .NotEqual(Guid.Empty)
               .WithMessage("MemberId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
