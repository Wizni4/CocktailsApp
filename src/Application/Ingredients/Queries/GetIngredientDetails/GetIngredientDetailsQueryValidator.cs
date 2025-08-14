
using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class GetIngredientDetailsQueryValidator : AbstractValidator<GetIngredientDetailsQuery>
    {
        public GetIngredientDetailsQueryValidator()
        {
            RuleFor(q => q.IngredientId).ValidGuid();
        }
    }
}
