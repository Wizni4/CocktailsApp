using Mono.Cecil;
using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.SeedWork
{
    public static class ConditionsExtensions
    {
        public static ConditionList MeetCondition(this Conditions conditions, Func<TypeDefinition, bool> condition)
        {
            var rule = new TypeDefinitionRule(condition);
            return conditions.MeetCustomRule(rule);
        }
    }
}
