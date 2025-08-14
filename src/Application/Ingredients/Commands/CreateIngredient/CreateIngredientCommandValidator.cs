using CocktailsApp.Application.Common;

using FluentValidation;


namespace CocktailsApp.Application.Ingredients
{
    public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientCommandValidator()
        {
            RuleFor(c => c.Name)
                .ValidString();
            RuleFor(c => c.Type)
                .ValidEnum();
            RuleFor(c => c.IsAlcoholic)
                .NotNull();
            When(c => c.Allergens is not null, () =>
            {
                RuleFor(c => c.Allergens!).ValidList();
                RuleForEach(c => c.Allergens!).ValidString();
            });
        }
    }
}
