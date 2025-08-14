
using CocktailsApp.Application.Common;

using FluentValidation;

namespace CocktailsApp.Application.Clubs
{
    public sealed class GetUserClubsQueryValidator : AbstractValidator<GetUserClubsQuery>
    {
        public GetUserClubsQueryValidator()
        {
            RuleFor(q => q.UserId).ValidGuid();
        }
    }
}
