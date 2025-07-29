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
    /// Validates the <see cref="CreateRoleCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoleCommandValidator"/> class.
        /// Defines validation rules for the <see cref="CreateRoleCommand"/>.
        /// </summary>
        public CreateRoleCommandValidator()
        {
            RuleFor(c => c.ClubId)
                .NotEqual(Guid.Empty)
                .WithMessage("ClubId must be a valid non-empty GUID.");

            RuleFor(c => c.RoleName)
                .NotNull()
                .WithMessage("RoleName must be provided.")
                .NotEmpty()
                .WithMessage("RoleName cannot be empty.");

            RuleFor(c => c.ActorId)
                .NotEqual(Guid.Empty)
                .WithMessage("ActorId must be a valid non-empty GUID.");
        }
    }
}
