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
    public class GlobalSearchQueryValidator : SearchQueryValidator<GlobalSearchQuery, GlobalSearchResultDTO>
    {
        public GlobalSearchQueryValidator()
        {
            RuleFor(c => c.UserId).ValidGuid();
        }
    }
}
