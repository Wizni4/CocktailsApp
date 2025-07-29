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
    /// Validates the <see cref="UpdateRoleNameCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateRoleNameCommandValidator : AbstractValidator<UpdateRoleNameCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRoleNameCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateRoleNameCommand"/>.
        /// </summary>
        public UpdateRoleNameCommandValidator()
        {
            RuleFor(c => c.ClubId)
               .NotEqual(Guid.Empty)
               .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleId)
                .NotEqual(Guid.Empty)
               .WithMessage("RoleId must be a valid non-empty GUID.");

            RuleFor(c => c.NewName)
                .NotNull()
                .WithMessage("NewName must be provided.")
                .NotEmpty()
                .WithMessage("NewName cannot be empty.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
