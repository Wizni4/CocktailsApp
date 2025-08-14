using CocktailsApp.Domain.Common;

using Mono.Cecil;

using NetArchTest.Rules;

using System.Reflection;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    [TestFixture]
    public class BuilderRulesTests
    {
        private Assembly _assembly;
        [SetUp]
        public void SetUp()
        {
            _assembly = Assembly.Load($"CocktailsApp.Domain");
        }
        [Test]
        public void Builder_MustInheritIBuilder()
        {
            // Act
            var result1 = Types
                .InAssembly(_assembly)
                .That()
                .HaveNameEndingWith("Builder")
                .Should()
                .MeetCustomRule(new BuilderInterfaceRule())
                .GetResult();

            var result2 = Types
                .InAssembly(_assembly)
                .That()
                .MeetCustomRule(new BuilderInterfaceRule())
                .Should()
                .HaveNameEndingWith("Builder")
                .GetResult();

            // Assert
            Assert.True(result1.IsSuccessful,
                "The following builders do not inherit IBuilder:\n" + string.Join("\n", result1?.FailingTypeNames ?? Enumerable.Empty<string>()));

            Assert.True(result2.IsSuccessful,
                "The following classes inherit IBuilder but their names do not end with 'Builder':\n" + string.Join("\n", result1?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }
    }

    public class BuilderInterfaceRule : ICustomRule
    {
        private const string IBuilderGeneric = "CocktailsApp.Domain.SeedWork.IBuilder`1";

        public bool MeetsRule(TypeDefinition type)
        {
            return type.Interfaces.Any(i =>
                i.InterfaceType is GenericInstanceType generic &&
                generic.ElementType.FullName == IBuilderGeneric);
        }
    }
}
