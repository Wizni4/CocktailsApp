/*
*Domain namespaces
*/
/*
 * Application namespaces
 */
using CocktailsApp.Application.SeedWork;

using FluentValidation;
/*
 * Framework namespaces
 */


namespace CocktailsApp.Application.Search
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        public SearchQueryValidator()
        {
            RuleFor(c => c.UserId).ValidGuid();
            RuleFor(c => c.Term).ValidString();
        }
    }
}
