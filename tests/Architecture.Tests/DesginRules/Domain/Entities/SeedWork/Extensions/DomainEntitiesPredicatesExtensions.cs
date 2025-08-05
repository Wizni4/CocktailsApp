using CocktailsApp.Domain.SeedWork;

using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public static class DomainEntitiesPredicatesExtensions
    {
        public static PredicateList AreEntities(this Predicates predicates)
        {
            return predicates
                .Inherit(typeof(Entity))
                .And()
                .DoNotInherit(typeof(AggregateRoot))
                .And()
                .DoNotImplementInterface(typeof(IAggregateRoot));
        }

        public static PredicateList AreAggregates(this Predicates predicates)
        {
            return predicates.Inherit(typeof(AggregateRoot));
        }

        public static PredicateList AreValueObjects(this Predicates predicates)
        {
            return predicates.Inherit(typeof(ValueObject));
        }
    }
}
