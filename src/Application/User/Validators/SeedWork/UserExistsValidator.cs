/*
 * Domain namespaces
 */
/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */
using CocktailsApp.Application.Cocktail;
using CocktailsApp.Application.SeedWork;

using DomainUser = CocktailsApp.Domain.UserAggregate.User;

namespace CocktailsApp.Application.User
{
    internal class UserExistsValidator(IRepository<DomainUser> userRepository) : AggregateExistsValidator<DomainUser>(userRepository)
    {
    }
}
