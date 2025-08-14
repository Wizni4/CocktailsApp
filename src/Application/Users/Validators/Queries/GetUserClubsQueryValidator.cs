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

using FluentValidation;


namespace CocktailsApp.Application.Users
{
    public class GetUserClubsQueryValidator : AbstractValidator<GetUserClubsQuery>
    {
        public GetUserClubsQueryValidator()
        {
            RuleFor(c => c.UserId)
                .ValidGuid();
        }
    }
}
