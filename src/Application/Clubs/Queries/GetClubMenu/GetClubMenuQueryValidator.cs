
using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubMenuQueryValidator : AbstractValidator<GetClubMenuQuery>
    {
        public GetClubMenuQueryValidator()
        {
            RuleFor(q => q.ClubId).ValidGuid();
        }
    }
}
