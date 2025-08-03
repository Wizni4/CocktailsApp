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

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.User
{
    public class GetUserClubsQueryValidator : AbstractValidator<GetUserClubsQuery>
    {
        public GetUserClubsQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.UserId)
                .ValidGuid()
                .IsUserExists(unitOfWork.Set<DomainUser>());
        }
    }
}
