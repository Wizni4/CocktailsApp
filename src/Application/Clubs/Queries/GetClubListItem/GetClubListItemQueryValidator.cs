using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubListItemQueryValidator : AbstractValidator<GetClubListItemQuery>
    {
        public GetClubListItemQueryValidator()
        {
            RuleFor(c => c.ClubId).ValidGuid();
        }
    }
}
