using CocktailsApp.Application.Common;

using FluentValidation;


namespace CocktailsApp.Application.Ingredients
{
    public class UploadIngredientImageCommandValidator : AbstractValidator<UploadIngredientImageCommand>
    {
        public UploadIngredientImageCommandValidator()
        {
            RuleFor(c => c.IngredientId).ValidGuid();
            RuleFor(c => c.Image).NotNull();
            RuleFor(c => c.ImageName).ValidString();
        }
    }
}
