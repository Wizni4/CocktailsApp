using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    public abstract class ClubCommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : IClubCommand
    {
        public ClubCommandValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
        }
    }
}
