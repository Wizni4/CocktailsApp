using CocktailsApp.Architecture.Tests.DesignRules.SeedWork;

using NetArchTest.Rules;

using System.Reflection;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    [TestFixture]
    public abstract class DomainRulesTests : BaseTests
    {
        [SetUp]
        public override void Setup()
        {
            Assembly = Assembly.Load($"CocktailsApp.Domain");
        }
    }
}
