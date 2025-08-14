using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="RemovePermissionsFromRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class RemovePermissionsFromRolesCommandValidator : ClubCommandValidator<RemovePermissionsFromRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemovePermissionsFromRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemovePermissionsFromRolesCommand"/>.
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
