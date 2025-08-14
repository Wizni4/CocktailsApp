using CocktailsApp.Application.Common;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace CocktailsApp.Application.Search
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        public SearchQueryValidator(IOptions<SearchOptions> options)
        {
            RuleFor(c => c.Term).ValidString();
            When(c => c.Limit != null, () =>
            {
                RuleFor(c => c.Limit)
                    .GreaterThan(0)
                    .LessThan(options.Value.MaxLimit);
            });
            When(c => c.Offset != null, () =>
            {
                RuleFor(c => c.Limit)
                    .GreaterThan(0);
            });
        }
    }
}
