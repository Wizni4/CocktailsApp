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
    /// Validates the <see cref="AddRoleToMemberCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddRoleToMemberCommandValidator : AbstractValidator<AddRoleToMemberCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoleToMemberCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddRoleToMemberCommand"/> properties.
        /// </summary>
        public AddRoleToMemberCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.MemberId)
                .NotEqual(Guid.Empty)
                .WithMessage("MemberId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleId)
                .NotEqual(Guid.Empty)
                .WithMessage("RoleId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
