using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Cocktails
{
    public class DeleteCocktailCommandValidator : AbstractValidator<DeleteCocktailCommand>
    {
        public DeleteCocktailCommandValidator()
        {
            RuleFor(c => c.Id).ValidGuid();
        }
    }
}
