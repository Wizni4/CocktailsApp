using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public static class DomainConditionsExtensions
    {
        public static ConditionList NotImplementInterfaces(this Conditions conditions)
        {
            var rule = new NotImplementInterfacesRule();
            return conditions.MeetCustomRule(rule);
        }

        public static ConditionList HaveImmutableAttributes(this Conditions conditions)
        {
            var rule = new HaveImmutableAttributesRule();
            return conditions.MeetCustomRule(rule);
        }

        public static ConditionList HaveInternalConstructor(this Conditions conditions)
        {
            var rule = new HaveInternalConstructorsRule();
            return conditions.MeetCustomRule(rule);
        }

        public static ConditionList NotHavePublicMethods(this Conditions conditions)
        {
            var rule = new NotHavePublicMethodsRule();
            return conditions.MeetCustomRule(rule);
        }
    }
}
