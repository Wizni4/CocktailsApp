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
    /// Validates the <see cref="UpdateRolesCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class UpdateRolesCommandValidator : AbstractValidator<UpdateRolesCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRolesCommandValidator"/> class.
        /// Defines validation rules for the <see cref="UpdateRolesCommand"/>.
        /// </summary>
        public UpdateRolesCommandValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
            RuleFor(c => c.ActorId).ValidGuid();
        }
    }
}
