using NetArchTest.Rules;


namespace CocktailsApp.Architecture.Tests.DesignRules.Domain
{
    [TestFixture]
    public class ValueObjectRulesTests : DomainRulesTests
    {
        private PredicateList ValueObjects { get; set; }

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ValueObjects = Types
                .InAssembly(Assembly)
                .That()
                .AreValueObjects();
        }

        [Test]
        public void ValueObjects_ShouldBeSealed()
        {
            var result = ValueObjects
             .Should()
             .BeSealed()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following value objects are not sealed:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void ValueObjects_ShouldNotImplementInterfaces()
        {
            var result = ValueObjects
             .Should()
             .NotImplementInterfaces()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following value objects inherit external interface:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void ValueObjects_ShouldNotHavePublicMethods()
        {
            var result = ValueObjects
             .Should()
             .NotHavePublicMethods()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following value objects contains public methods:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }

        [Test]
        public void ValueObjects_ShouldHaveImmutableAttributes()
        {
            var result = ValueObjects
             .Should()
             .HaveImmutableAttributes()
             .GetResult();

            Assert.True(result.IsSuccessful,
                "The following value objects contains mutable properties:\n" + string.Join("\n", result?.FailingTypeNames ?? Enumerable.Empty<string>()));
        }
    }
}
