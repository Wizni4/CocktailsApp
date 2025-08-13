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
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="RemovePermissionsFromRoleCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemovePermissionsFromRolesCommandValidator : ClubCommandValidator<RemovePermissionsFromRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePermissionsFromRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemovePermissionsFromRoleCommand"/>.
        /// </summary>
        public RemovePermissionsFromRolesCommandValidator()
        {
            RuleFor(c => c.Roles).ValidList();
            RuleForEach(c => c.Roles)
                .ChildRules(a =>
                {
                    a.RuleFor(r => r.Id).ValidGuid();
                    a.RuleFor(r => r.Permissions).ValidList();
                    a.RuleForEach(r => r.Permissions).ValidEnum();
                });
        }
    }
}
