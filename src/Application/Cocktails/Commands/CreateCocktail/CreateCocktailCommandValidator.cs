using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Cocktails
{
    public class CreateCocktailCommandValidator : AbstractValidator<CreateCocktailCommand>
    {
        public CreateCocktailCommandValidator()
        {
            When(c => c.Description is not null, () =>
            {
                RuleFor(c => c.Description).ValidString();

            });
            RuleFor(c => c.Ingredients).ValidList();
            RuleForEach(c => c.Ingredients)
                .ChildRules(a =>
                {
                    a.RuleFor(i => i.Id).ValidGuid();
                    a.RuleFor(i => i.Quantity).GreaterThan(0);
                    a.RuleFor(i => i.Unit).ValidEnum();
                });
        }
    }
}
