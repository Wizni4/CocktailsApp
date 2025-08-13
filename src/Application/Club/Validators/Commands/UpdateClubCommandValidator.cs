/*
 * Domain namespaces
 */

/*
 * Application namespaces
 */

/*
 * Framework namespaces
 */
using CocktailsApp.Application.SeedWork;
using CocktailsApp.Domain.ClubAggregate;

using FluentValidation;

namespace CocktailsApp.Application.Club
{
    public class UpdateClubCommandValidator : ClubCommandValidator<UpdateClubCommand>
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
