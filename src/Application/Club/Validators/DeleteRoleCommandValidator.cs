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
    /// Validates the <see cref="DeleteRoleCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleCommandValidator"/> class.
        /// Defines validation rules for the <see cref="DeleteRoleCommand"/>.
        /// </summary>
        public DeleteRoleCommandValidator()
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
        }
    }
}
