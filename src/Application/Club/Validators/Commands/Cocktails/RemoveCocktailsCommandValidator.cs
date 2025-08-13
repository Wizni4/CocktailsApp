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
    /// Validates the <see cref="RemoveCocktailsCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class RemoveCocktailsCommandValidator : ClubCommandValidator<RemoveCocktailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCocktailsCommandValidator"/> class.
        /// Defines validation rules for the <see cref="RemoveCocktailsCommand"/>.
        /// </summary>
        public RemoveCocktailsCommandValidator()
        {
            RuleFor(c => c.CocktailIds).ValidList();
            RuleForEach(c => c.CocktailIds).ValidGuid();
        }
    }
}
