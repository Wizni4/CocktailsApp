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
    /// Validates the <see cref="DeleteRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class DeleteRolesCommandValidator : ClubCommandValidator<DeleteRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="DeleteRolesCommand"/>.
        /// </summary>
        public DeleteRolesCommandValidator(IClubRepository clubRepository)
            : base(clubRepository, [ClubPermissionType.DeleteRole])
        {
            RuleFor(c => c.RoleIds).ValidList();
            RuleForEach(c => c.RoleIds).ValidGuid();
        }
    }
}
