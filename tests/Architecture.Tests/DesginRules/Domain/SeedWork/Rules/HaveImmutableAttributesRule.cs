using CocktailsApp.Architecture.Tests.DesignRules.SeedWork;

namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    public class HaveImmutableAttributesRule : TypeDefinitionRule
    {
        private static readonly string[] s_allowedTypes = new[]
        {
            "System.Collections.Generic.IReadOnlyList`1",
            "System.Collections.Generic.IReadOnlyCollection`1"
        };

        public HaveImmutableAttributesRule()
            : base(type => 
                type.Fields
                    .All(f => !f.IsPublic || f.IsInitOnly) &&
                type.Properties
                    .Where(p => p.GetMethod != null && p.GetMethod.IsPublic)
                    .All(p => p.SetMethod == null || !p.SetMethod.IsPublic) &&
                type.Properties
                    .Where(p => p.GetMethod != null && p.GetMethod.IsPublic)
                    .Where(p => p.PropertyType.IsGenericInstance)
                    .All(p => s_allowedTypes.Contains(p.PropertyType.GetElementType().FullName)))
        { }
    }
}
