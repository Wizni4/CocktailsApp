using CocktailsApp.Application.Common;

using FluentValidation;


namespace CocktailsApp.Application.Clubs
{
    public sealed class GetClubDetailsQueryValidator : AbstractValidator<GetClubDetailsQuery>
    {
        public GetClubDetailsQueryValidator()
        {
            RuleFor(q => q.ClubId).ValidGuid();
        }
    }
}
