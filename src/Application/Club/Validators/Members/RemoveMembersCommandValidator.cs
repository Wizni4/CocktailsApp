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
    /// Validates the <see cref="RemoveMembersCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveMembersCommandValidator : AbstractValidator<RemoveMembersCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveMembersCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveMembersCommand"/>.
        /// </summary>
        public RemoveMembersCommandValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
            RuleFor(c => c.MemberIds).ValidList();
            RuleForEach(c => c.MemberIds).ValidGuid();
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
