
using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="AddPermissionsToRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class AddPermissionsToRolesCommandValidator : ClubCommandValidator<AddPermissionsToRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPermissionsToRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddPermissionsToRolesCommand"/> properties.
        /// </summary>
        public AddPermissionsToRolesCommandValidator()
        {
            RuleFor(c => c.Roles).ValidList();
            RuleForEach(c => c.Roles).SetValidator(new RolePermissionsUpdateModelValidator());
        }
    }
}
