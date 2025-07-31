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
    /// Validates the <see cref="RemoveRolesFromMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveRolesFromMembersCommandValidator : AbstractValidator<RemoveRolesFromMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRolesFromMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveRolesFromMembersCommand"/>.
        /// </summary>
        public RemoveRolesFromMembersCommandValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
            RuleFor(c => c.Members).ValidList();
            RuleForEach(c => c.Members).SetValidator(new MemberRolesUpdateModelValidator());
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
