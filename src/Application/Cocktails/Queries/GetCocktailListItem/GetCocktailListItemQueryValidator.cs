

using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Cocktails
{
    public sealed class GetCocktailListItemQueryValidator : AbstractValidator<GetCocktailListItemQuery>
    {
        public GetCocktailListItemQueryValidator()
        {
            RuleFor(q => q.CocktailId).ValidGuid();
        }
    }
}
