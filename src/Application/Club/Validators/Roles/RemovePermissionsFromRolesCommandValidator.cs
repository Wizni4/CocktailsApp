/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="RemovePermissionsFromRoleCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemovePermissionsFromRolesCommandValidator : AbstractValidator<RemovePermissionsFromRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePermissionsFromRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemovePermissionsFromRoleCommand"/>.
        /// </summary>
        public RemovePermissionsFromRolesCommandValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
