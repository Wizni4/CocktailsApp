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
    /// Validates the <see cref="AddRolesToMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddRolesToMembersCommandValidator : ClubCommandValidator<AddRolesToMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddRolesToMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddRolesToMembersCommand"/> properties.
        /// </summary>
        public AddRolesToMembersCommandValidator()
        {
            RuleFor(c => c.Members).ValidList();
            RuleForEach(c => c.Members).SetValidator(new MemberRolesUpdateModelValidator());
        }
    }
}
