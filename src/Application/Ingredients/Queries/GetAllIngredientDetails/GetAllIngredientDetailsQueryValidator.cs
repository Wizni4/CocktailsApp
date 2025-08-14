
using CocktailsApp.Application.Common;

using FluentValidation;

using Microsoft.Extensions.Options;


namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetAllIngredientDetailsQueryValidator : AbstractValidator<GetAllIngredientDetailsQuery>
    {
        public GetAllIngredientDetailsQueryValidator(IOptions<PageOptions> options)
        {
            When(q => q.Offset != null, () =>
            {
                RuleFor(q => q.Offset)
                    .GreaterThan(0);
            });
            When(q => q.Limit != null, () =>
            {
                RuleFor(q => q.Limit)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(options.Value.MaxLimit);
            });
        }
    }
}
