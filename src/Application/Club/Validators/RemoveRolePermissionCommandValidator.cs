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
    /// Validates the <see cref="RemoveRolePermissionCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveRolePermissionCommandValidator : AbstractValidator<RemoveRolePermissionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRolePermissionCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveRolePermissionCommand"/>.
        /// </summary>
        public RemoveRolePermissionCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleId)
                .NotEqual(Guid.Empty)
               .WithMessage("RoleId must be a valid non-empty GUID.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");

            RuleFor(c => c.Permission)
               .IsInEnum()
               .WithMessage("Permission must be a valid enum value.");
        }
    }
}
