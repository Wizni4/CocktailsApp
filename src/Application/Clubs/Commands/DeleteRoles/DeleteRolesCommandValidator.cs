using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="DeleteRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class DeleteRolesCommandValidator : ClubCommandValidator<DeleteRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="DeleteRolesCommand"/>.
        /// </summary>
        public DeleteRolesCommandValidator()
        {
            RuleFor(c => c.RoleIds).ValidList();
            RuleForEach(c => c.RoleIds).ValidGuid();
        }
    }
}
