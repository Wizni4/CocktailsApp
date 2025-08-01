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
    public class RolePermissionsUpdateModelValidator : AbstractValidator<RolePermissionsUpdateModel>
    {
        public RolePermissionsUpdateModelValidator()
        {
            RuleFor(c => c.Id).ValidGuid();
            RuleFor(c => c.Permissions).ValidList();
            RuleForEach(c => c.Permissions).ValidEnum();
        }
    }
}
