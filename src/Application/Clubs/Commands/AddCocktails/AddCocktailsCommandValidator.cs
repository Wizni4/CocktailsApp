using CocktailsApp.Application.Common;


namespace CocktailsApp.Application.Clubs
{
    /// <summary>
    /// Validates the <see cref="AddCocktailsCommand"/> to ensure all required identifiers are provided and valid.
    /// </summary>
    public sealed class AddCocktailsCommandValidator : ClubCommandValidator<AddCocktailsCommand>
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
