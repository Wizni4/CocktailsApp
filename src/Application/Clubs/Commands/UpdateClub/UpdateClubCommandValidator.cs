

using CocktailsApp.Application.Common;

namespace CocktailsApp.Application.Clubs
{
    public sealed class UpdateClubCommandValidator : ClubCommandValidator<UpdateClubCommand>
    {
        public UpdateClubCommandValidator()
        {
            When(c => c.Address is not null, () =>
            {
                RuleFor(c => c.Address!).ValidAddress();
            });

            When(c => c.Name is not null, () =>
            {
                RuleFor(c => c.Name).ValidString();
            });

            When(c => c.Description is not null, () =>
            {
                RuleFor(c => c.Description).ValidString();
            });

            When(c => c.Visibility is not null, () =>
            {
                RuleFor(c => c.Visibility).ValidEnum();
            });
        }
    }
}
