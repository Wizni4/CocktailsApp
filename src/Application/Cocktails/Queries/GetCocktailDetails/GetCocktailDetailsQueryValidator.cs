using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class GetCocktailDetailsQueryValidator : AbstractValidator<GetCocktailDetailsQuery>
    {
        public GetCocktailDetailsQueryValidator()
        {
            RuleFor(q => q.CocktailId).ValidGuid();
        }
    }
}
