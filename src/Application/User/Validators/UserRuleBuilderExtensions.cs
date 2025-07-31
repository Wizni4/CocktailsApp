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
using FluentValidation;

namespace CocktailsApp.Application.User
{
    public static class UserRuleBuilderExtensions
    {
        public static IRuleBuilder<T, Guid> IsUserExists<T>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<DomainUser> userRepository)
        {
            return ruleBuilder.SetValidator(new UserExistsValidator(userRepository));
        }
    }
}
