
using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Ingredients
{
    public sealed class DeleteIngredientCommandValidator : AbstractValidator<DeleteIngredientCommand>
    {
        public DeleteIngredientCommandValidator()
        {
            RuleFor(i => i.Id).ValidGuid();
        }
    }
}
