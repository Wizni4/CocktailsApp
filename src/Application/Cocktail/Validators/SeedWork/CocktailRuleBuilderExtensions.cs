
/*
 * Application namespaces
 */
/*
 * Framework namespaces
 */

/*
 * Domain namespaces
 */
using CocktailsApp.Application.SeedWork;
using FluentValidation;
using DomainCocktail = CocktailsApp.Domain.CocktailAggregate.Cocktail;


namespace CocktailsApp.Application.Cocktail
{
    public static class CocktailRuleBuilderExtensions
    {
        public static IRuleBuilder<T, Guid> IsCocktailExists<T>(this IRuleBuilder<T, Guid> ruleBuilder, IRepository<DomainCocktail> cocktailRepository)
        {
            return ruleBuilder.SetValidator(new CocktailExistsValidator(cocktailRepository));
        }
    }
}
