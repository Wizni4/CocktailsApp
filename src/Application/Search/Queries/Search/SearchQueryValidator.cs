using CocktailsApp.Application.Common;
using FluentValidation;

namespace CocktailsApp.Application.Search
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        public SearchQueryValidator()
        {
            RuleFor(c => c.Term).ValidString();
            When(c => c.Limit != null, () =>
            {
                RuleFor(c => c.Limit!.Value).GreaterThan(0);
                RuleFor(c => c.Limit!.Value).LessThan(100);
            });
            When(c => c.Offset != null, () =>
            {
                RuleFor(c => c.Limit!.Value).GreaterThan(0);
            });
        }
    }
}
