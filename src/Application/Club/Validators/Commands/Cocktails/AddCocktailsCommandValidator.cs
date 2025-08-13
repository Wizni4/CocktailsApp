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


namespace CocktailsApp.Application.Club
{
    /// <summary>
    /// Validates the <see cref="AddCocktailsCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public class AddCocktailsCommandValidator : ClubCommandValidator<AddCocktailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCocktailsCommandValidator"/> class.
        /// Defines validation rules for the <see cref="AddCocktailsCommand"/> properties.
        /// </summary>
        public AddCocktailsCommandValidator()
        {
            RuleFor(c => c.CocktailIds).ValidList();
            RuleForEach(c => c.CocktailIds).ValidGuid();
        }
    }
}
