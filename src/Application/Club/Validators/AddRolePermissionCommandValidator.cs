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
    /// Validates the <see cref="AddRolePermissionCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddRolePermissionCommandValidator : AbstractValidator<AddRolePermissionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRolePermissionCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddRolePermissionCommand"/> properties.
        /// </summary>
        public AddRolePermissionCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleId)
                .NotEqual(Guid.Empty)
                .WithMessage("RoleId must be a valid non-empty GUID.");

            RuleFor(c => c.Permission)
                .IsInEnum()
                .WithMessage("Permission must be a valid enum value.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
